using UnityEngine;

public class TimerController : MonoBehaviour
{
    public Timer timerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (timerScript.isCounting)
        {
            timerScript.StopTimer();
            Destroy(this.gameObject);
        }
        else
        {
            timerScript.StartTimer();
            Destroy(this.gameObject);
        }
    }
}
