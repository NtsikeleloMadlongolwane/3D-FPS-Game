using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private AudioSource audioSource;


        private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FPController fP = other.gameObject.GetComponent<FPController>();
            fP.spawnLocation = this.gameObject.transform.position;

            audioSource.Play();
        }


    }
}
