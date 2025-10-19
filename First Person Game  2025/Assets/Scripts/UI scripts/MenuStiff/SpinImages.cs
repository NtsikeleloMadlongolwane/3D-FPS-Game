using UnityEngine;

public class SpinImages : MonoBehaviour
{
    public float speed = 100f;
    public bool clockwise = true;

    private void Update()
    {
        float direction = clockwise ? -1f : 1f;
        transform.Rotate(0f, 0f, direction * speed * Time.deltaTime);
    }
}
