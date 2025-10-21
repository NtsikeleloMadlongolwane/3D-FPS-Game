using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public string targetTag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag))
        {
            Destroy(other.gameObject);
        }
    }
}