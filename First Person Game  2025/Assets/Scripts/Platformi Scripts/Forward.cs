using UnityEngine;

public class Forward : MonoBehaviour
{

    public float speed = 5f;
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

}
