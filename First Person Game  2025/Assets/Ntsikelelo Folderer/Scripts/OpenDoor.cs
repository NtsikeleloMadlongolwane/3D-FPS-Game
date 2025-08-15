using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public float raiseAmount = 3f;
    public float raiseSpeed = 2f;
    private Vector3 targetPosition;
    private bool isOpening = false;

    private void Awake()
    {
        targetPosition = transform.position + Vector3.up * raiseAmount;
    }
    private void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, raiseSpeed * Time.deltaTime);
            if(transform.position == targetPosition)
            {
                isOpening = false;
                Debug.Log("Door is Opening");
            }
        }
    }
    public void OpenTheDoor()
    {
        isOpening = true;
    }
}
