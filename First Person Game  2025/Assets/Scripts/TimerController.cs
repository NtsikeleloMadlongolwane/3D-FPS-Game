using UnityEngine;

public class TimerController : MonoBehaviour
{
    public Timer timerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (timerScript.isCounting)
            {

                timerScript.StopTimer();
                Destroy(this.gameObject);

                // reveal cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                timerScript.StartTimer();
                Destroy(this.gameObject);
            }
        } 
    }
}
