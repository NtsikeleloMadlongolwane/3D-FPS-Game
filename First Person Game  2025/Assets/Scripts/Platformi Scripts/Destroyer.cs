using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public string tag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag))
        {
            Destroy(other.gameObject);
        }
    }
}