using UnityEngine;

public class Spinning : MonoBehaviour
{
    public float spinSpeed = 100f;
    private bool canSpin = true;
    public GameObject solidGround;
    void Update()
    {
        if (canSpin)
        {
            transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Switch"))
        {
            canSpin = false;
            solidGround.SetActive(true);
}
    }
    public void OnTriggerExit(Collider other)
    {
        canSpin = true;
        solidGround.SetActive(false);
    }
}
