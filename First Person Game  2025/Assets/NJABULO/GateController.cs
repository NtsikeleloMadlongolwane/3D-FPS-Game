using UnityEngine;

public class GateController : MonoBehaviour
{
    public Transform gateLeft;      // Assign in Inspector
    public Transform gateRight;     // Assign in Inspector
    public float openDistance = 5f; // How far each gate slides
    public float moveSpeed = 2f;    // Movement speed

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;
    private Vector3 leftOpenPos;
    private Vector3 rightOpenPos;
    private bool isOpen = false;

    void Start()
    {
        // Store initial positions
        leftClosedPos = gateLeft.position;
        rightClosedPos = gateRight.position;

        // Calculate open positions using local right direction
        leftOpenPos = leftClosedPos - gateLeft.right * openDistance;
        rightOpenPos = rightClosedPos + gateRight.right * openDistance;
    }

    void Update()
    {
        if (isOpen)
        {
            gateLeft.position = Vector3.MoveTowards(gateLeft.position, leftOpenPos, moveSpeed * Time.deltaTime);
            gateRight.position = Vector3.MoveTowards(gateRight.position, rightOpenPos, moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = true;
        }
    }
}
