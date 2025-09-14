using UnityEngine;

public class HazardRespawn : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FPController fP = other.gameObject.GetComponent<FPController>();
            fP.Respawn();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FPController fP = collision.gameObject.GetComponent<FPController>();
            fP.Respawn();
            Destroy(this.gameObject);
        }
    }
}
