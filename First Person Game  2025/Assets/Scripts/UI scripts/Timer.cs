using UnityEngine;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI centerTextCounter;
    public TextMeshProUGUI centerTextInfo;

    public FPController fPController;

    private float timer = 0f;
    private bool isCounting = false;

    private void Update()
    {
        if (isCounting)
        {
            timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            int miliseconds = Mathf.FloorToInt((timer * 1000f) % 1000f);

            timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SartTimer();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            StopTimer();
        }
    }


    public void SartTimer()
    {
        timer = 0f;
        centerTextInfo.text = "";
        centerTextCounter.text ="";
        isCounting = true;
        fPController.canMove = true;
    }
    public void StopTimer()
    {
        isCounting = false;
        centerTextInfo.text = "You have finished the level!\nYour Time Was:";
        centerTextCounter.text = timerText.text;
        fPController.canMove = false;
    }
}
