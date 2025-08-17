using UnityEngine;

public class SpringBoard : MonoBehaviour
{
    private FPController fPController;
    public float springPower = 10f;
    private void Awake()
    {
        fPController = FindAnyObjectByType<FPController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fPController.SpringBoard(springPower);
        }
    }
}
