using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject Door;
    public OpenDoor openDoor;

    public void Awake()
    {
        openDoor = Door.GetComponent<OpenDoor>();
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Switch"))
        {
            if (openDoor != null)
            {
                openDoor.OpenTheDoor();
            }
        }
    }
}
