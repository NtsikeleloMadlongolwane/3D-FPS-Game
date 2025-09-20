using UnityEngine;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI centerTextCounter;
    public TextMeshProUGUI centerTextInfo;

    public GameObject WinScreen;

    public FPController fPController;

    public bool isCounting = false;

    private float timer = 0f;

    public void Start()
    {
        WinScreen.SetActive(false);
    }
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

    }


    public void StartTimer()
    {
        timer = 0f;
        centerTextInfo.text = "";
        centerTextCounter.text ="";
        WinScreen.SetActive(false);
        isCounting = true;
        fPController.canMove = true;
    }
    public void StopTimer()
    {
        isCounting = false;
        centerTextInfo.text = "You have finished the level!\nYour Time Was:";
        WinScreen.SetActive(true);
        centerTextCounter.text = timerText.text;
        fPController.canMove = false;
    }
}
