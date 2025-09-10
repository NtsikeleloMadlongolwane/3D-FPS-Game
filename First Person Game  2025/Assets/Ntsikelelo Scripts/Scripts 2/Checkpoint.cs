using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FPController fP = other.gameObject.GetComponent<FPController>();
            fP.spawnLocation = this.gameObject.transform.position;
        }
    }
}
