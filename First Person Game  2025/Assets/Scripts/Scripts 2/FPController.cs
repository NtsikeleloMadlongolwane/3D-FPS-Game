using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class FPController : MonoBehaviour
{
    [Header("Movement Settings")]
    public bool canMove = true;
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Look Settings")]
    public bool canLook = true;
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

    public Transform laserPoint;
    public GameObject pinkBullet;
    public float laserSpeed;

    [Header("Double Jump")]
    public bool canDoubleJump = false;
    public int maxJumpCount = 2;
    public int jumpCount = 0;

    [Header("Dash Setting")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 2f;
    public TextMeshProUGUI cooldownText;
    public GameObject dashReady;
    public GameObject dashNotReady;

    private bool isDashing = false;
    private float dashTime;
    private float cooldownTimer = 0f;

    [Header("Respawn")]
    public Vector3 spawnLocation;

    [Header("Quit and Restart")]
    public SceneManageemment sM;
    public MainMenuRoutines mMR;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    [Header("Pause Settings")]
    public bool isPaused = false;
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
//DOUBLE JUMP // Might remove later
        if(controller.isGrounded && velocity.y <= 0)
        {
            jumpCount = 0;
        }

// DASHING //
        if (isDashing)
        {
            Dash();
        }
        // dash cooldown
        if (cooldownTimer > 0)
        {
            dashReady.SetActive(false);
            dashNotReady.SetActive(true);
            cooldownTimer -= Time.deltaTime;
            cooldownText.text = $"{cooldownTimer:F1}s"; 
        }
        else
        {
            cooldownText.text = "";
            dashReady.SetActive(true);
            dashNotReady.SetActive(false);
        }


    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (!canMove) return;
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!canLook) return;

        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!canMove) return;

            if (context.performed && jumpCount < maxJumpCount)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jumpCount++;
            }
    }

    public void HandleMovement()
    {
        if (!canMove) return;

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void HandleLook()
    {
        //if (!canMove) return;

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

        if (bulletPrefab != null && gunPoint != null && gunPoint.transform.name == "ShootPoint")
        {
            if (gunPoint == null) return;

            GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            // calculate dorection

            Vector3 forceDirection = cameraTransform.transform.forward;
            RaycastHit hit;
            if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 500f))
            {
                forceDirection = (hit.point - gunPoint.position).normalized;
            }
            // direction calculate
            if(rb != null)
            {
                rb.AddForce(forceDirection * bulletSpeed);
            }
           
            Destroy(bullet, 3f);
        } // shooting

       if (canSwich)
        {
            TogoClap();
        }
       
        // todo gun

        if(heldObject != null && heldObject.CompareTag("Switch"))
        {
            GameObject throwable = heldObject.gameObject;
            Rigidbody trb = throwable.GetComponent<Rigidbody>();

            heldObject.Drop();
            heldObject = null;

            Vector3 throwDirection = holdPoint.forward + holdPoint.up * throwUpwardBoost;

            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 500f))
            {
                throwDirection = (hit.point - holdPoint.position).normalized;
            }

            trb.AddForce(throwDirection.normalized * throwForce, ForceMode.Impulse);
            trb = null;

            Debug.Log("Threw Ball");
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
                        if (hit.collider.CompareTag("TodoGun"))
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

    public void SetGunEffect(InputAction.CallbackContext context)
    {
        if (!canSwich) return;
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 500f))
        {
            if (hit.collider.CompareTag("Switch"))
            {
                if (markedObject != null)
                {
                    Marking ummark = markedObject.GetComponent<Marking>();
                    ummark.Unmarked();
                }

                markedObject = hit.collider.gameObject;
                savedPosition = hit.transform.position;
                //Debug.Log("Switch Location is now: " + savedPosition);

                Marking mark = hit.collider.GetComponent<Marking>();
                mark.Marked();
            }
        }
    }
    public void TogoClap()
    {
        if (!canSwich) return;
        if (!canMove) return;
        if (savedPosition == null) return;

        Vector3 tempLocationStore = this. transform.position;

        if(markedObject != null)
        {
            controller.enabled = false;
            this.transform.position = markedObject.transform.position;
            controller.enabled = true;
            markedObject.transform.position = tempLocationStore;
        }        
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

    public void StartDash(InputAction.CallbackContext context)
    {
        if(!isDashing && cooldownTimer <= 0)
        {
            isDashing = true;
            dashTime = dashDuration;
            cooldownTimer = dashCooldown;
        }
    }
    public void Dash()
    {
        if (!canMove) return;
        if (dashTime > 0)
        {
            Vector3 dashDirection = transform.forward;
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
            dashTime -= Time.deltaTime;
        }
        else
        {
            isDashing = false;
        }
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
       // if (!canMove) return;
        HandlePause();      
    }

    public void HandlePause()
    {
        if (isPaused == false)
        {
            Time.timeScale = 0;
            isPaused = true;

            canMove = false;
            canLook = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            mMR.PauseMenu();
            mMR.ClearPauseMenu();
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;

            canMove = true;
            canLook = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;

            mMR.PauseMenu();
            mMR.ClearPauseMenu();
        }

    }

}


