using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawningObject;
    public float spawnInterval = 1f;
    public Transform spawnPoint;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnInterval)
        {
            Instantiate(spawningObject, spawnPoint.position, spawnPoint.rotation);
            timer = 0f;
        }
    }
}
