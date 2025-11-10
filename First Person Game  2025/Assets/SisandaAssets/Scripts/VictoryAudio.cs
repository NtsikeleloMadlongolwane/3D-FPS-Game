using UnityEngine;

public class VictoryAudio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
                audioSource.Play();
    }
}
