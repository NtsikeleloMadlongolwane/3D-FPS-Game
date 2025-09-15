using UnityEngine;

public class Hammers : MonoBehaviour
{

    [Header("Swing Settings")]
    public float swingAngle = 45f;
    public float swingSpeed = 2f; 
    public Vector3 rotationAxis = Vector3.forward; 

    private float startAngle;
    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.rotation;
        startAngle = transform.eulerAngles.z;
    }

    void Update()
    {
        float angle = Mathf.Sin(Time.time * swingSpeed) * swingAngle;
        transform.rotation = startRotation * Quaternion.AngleAxis(angle, rotationAxis);
    }

}
