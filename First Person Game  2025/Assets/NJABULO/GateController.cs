using UnityEngine;

public class GateController : MonoBehaviour
{
    public Transform gateLeft;      
    public Transform gateRight;     
    public float openDistance = 5f; 
    public float moveSpeed = 2f;   

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;
    private Vector3 leftOpenPos;
    private Vector3 rightOpenPos;
    private bool isOpen = false;

    public AudioSource audioSource;  
     

    void Start()
    {
        
        leftClosedPos = gateLeft.position;
        rightClosedPos = gateRight.position;

      
        leftOpenPos = leftClosedPos - gateLeft.right * openDistance;
        rightOpenPos = rightClosedPos + gateRight.right * openDistance;
    }

    void Update()
    {
        audioSource = GetComponent<AudioSource>();

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

            audioSource.Play();
        }
    }
}
