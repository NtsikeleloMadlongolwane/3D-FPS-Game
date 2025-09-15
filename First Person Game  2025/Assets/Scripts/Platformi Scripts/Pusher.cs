using UnityEngine;

public class Pusher : MonoBehaviour
{
    [Header("Push Settings")]
    public float pushForce = 5f;
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
            {
                // Calculate push direction from this object to the player
                Vector3 pushDirection = (other.transform.position - transform.position).normalized;

                // Apply movement using CharacterController
                controller.Move(pushDirection * pushForce);
            }
        }
    }

}
