using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public string targetTag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            Destroy(other.gameObject);
            Debug.Log("Object Destroyed");
        }
    }
}