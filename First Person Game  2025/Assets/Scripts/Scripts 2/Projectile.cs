using UnityEngine;

public class Projectile : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Gun"))
        {
            Rigidbody rb = this.GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }
}
