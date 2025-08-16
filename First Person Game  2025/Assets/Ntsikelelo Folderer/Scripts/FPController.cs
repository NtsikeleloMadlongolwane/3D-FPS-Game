using UnityEngine;
using UnityEngine.InputSystem;

public class FPController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float verticalLookLimit = 90f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform gunPoint;
    public float bulletSpeed = 1000f;

    [Header("Crouch Settings")]
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    public float crouchSpeed = 2.5f;
    private float originalMoveSpeed;

    [Header("PickUp Settings")]
    public float pickupRange = 3f;
    public Transform holdPoint;
    private PickUpObject heldObject;

    [Header("Throwing Settings")]
    public float throwForce = 10f;
    public float throwUpwardBoost = 1f;

    [Header("Swich Gun Settings")]
    public bool canSwich = false;
    public GameObject markedObject;
    public GameObject oldObject;
    public Vector3 savedPosition;
    public Transform playerLocation;

    [Header("Duble Jump")]
    public int maxJumpCount = 2;
    public int jumpCount = 0;

    [Header("Respawn")]
    public Vector3 spawnLocation;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        originalMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();

        if(heldObject != null)
        {
            heldObject.MoveToHoldPoint(holdPoint.position);
        }

        if(controller.isGrounded && velocity.y <= 0)
        {
            jumpCount = 0;
        }
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && jumpCount < maxJumpCount)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpCount++;
        }
    }

    public void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        if(bulletPrefab != null && gunPoint != null)
        {
            if (gunPoint == null) return;

            GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            // calculate dorection

            Vector3 forceDirectsion = cameraTransform.transform.forward;
            RaycastHit hit;
            if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 500f))
            {
                forceDirectsion = (hit.point - gunPoint.position).normalized;
            }
            // direction calculate
            if(rb != null)
            {
                rb.AddForce(forceDirectsion * bulletSpeed);
            }
           
            Destroy(bullet, 3f);
        }

        if (canSwich)
        {
            TogoClap();
            Debug.Log("Swiched");
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.height = crouchHeight;
            moveSpeed = crouchSpeed;
        }
        else if (context.canceled)
        {
            controller.height = standHeight;
            moveSpeed = originalMoveSpeed;
        }
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if(heldObject == null)
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if(Physics.Raycast(ray, out RaycastHit hit, pickupRange))
            {
                PickUpObject pickUp = hit.collider.GetComponent<PickUpObject>();
                if (pickUp != null)
                {
                    pickUp.PickUp(holdPoint);
                    heldObject = pickUp;

                    if (hit.collider.CompareTag("Gun") && hit.collider.transform.name != "Teleport Gun")
                    {
                        Transform childTransform = hit.collider.transform.GetChild(0);
                        gunPoint = childTransform;
                    }
                    if(hit.collider.transform.name == "Teleport Gun")
                    {
                        canSwich = true;
                        Debug.Log("Teleport Gun Equipped");
                    }

                }
            }
        }

        else
        {
            heldObject.Drop();
            heldObject = null;

            gunPoint = null;
            canSwich = false;
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (heldObject == null) return;

        Vector3 dir = cameraTransform.forward;
        Vector3 impuse = dir * throwForce + Vector3.up * throwUpwardBoost;

        // heldObject.Throw(impulse);
        heldObject = null;
    }


    public void SetGunEffect(InputAction.CallbackContext context)
    {
        if (canSwich)
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, 500f))
            {
                if (hit.collider.CompareTag("Switch"))
                {
                    if(markedObject!= null)
                    {
                        Marking ummark = markedObject.GetComponent<Marking>();
                        ummark.Unmarked();
                    }

                    markedObject = hit.collider.gameObject;
                    savedPosition = hit.transform.position;
                    Debug.Log("Switch Location is now: " + savedPosition);

                    Marking mark = hit.collider.GetComponent<Marking>();
                    mark.Marked();
                }
            }
        }
    }
    public void TogoClap()
    {
        if (savedPosition == null) return;

        Vector3 tempLocationStore = this. transform.position;

        controller.enabled = false;
        this.transform.position = markedObject.transform.position;
        controller.enabled = true;
        markedObject.transform.position = tempLocationStore;       
    }
    public void SpringBoard(float springPower)
    {
            velocity.y = Mathf.Sqrt(springPower * -2f * gravity);
    }

    public void Respawn()
    {
        controller.enabled = false;
        this.transform.position = spawnLocation;
        controller.enabled = true;
    }
}


