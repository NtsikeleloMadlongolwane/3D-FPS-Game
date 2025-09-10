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
}
