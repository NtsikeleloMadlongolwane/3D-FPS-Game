using UnityEngine;
using System.Collections;

public class Hovering : MonoBehaviour
{
    [Header("Hover Settings")]
    public float hoverHeight = 1.5f;
  
    private Vector3 startPos;
    private float timer = 0f;
    private float timeDelay = 0f;
    private float hoverSpeed = 2f;
    void Start()
    {
        startPos = transform.position;
        timeDelay = Random.Range(0.3f, 1.1f);
        hoverSpeed = Random.Range(1.5f, 3f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > timeDelay)
        {
            float newY = startPos.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
            transform.position = new Vector3(startPos.x, newY, startPos.z);
        }
    }
}
