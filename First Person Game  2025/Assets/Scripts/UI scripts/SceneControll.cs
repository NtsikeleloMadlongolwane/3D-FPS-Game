using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneControll : MonoBehaviour
{
    [Header("Player Control")]
    public FPController fPController;
    public TextMeshProUGUI centerText;
    public TextMeshProUGUI timerText;
    public GameObject timeUpPanel; 
    public Button retryButton;  

     [Header("Timer Settings")]
    public float levelDuration = 300f;         // 5 minutes (in seconds)

    private float currentTime = 0f;
    private bool isCounting = false;
    private bool timeUp = false;

    private void Start()
    {

        timerText.text = "00:00";
        timeUpPanel.SetActive(false);

        fPController.canMove = false;

        retryButton.onClick.AddListener(RestartLevel);
   
        StartCoroutine(LevelCountDown());

    }

    private void Update()
    {
        // Timer counts up only if allowed
        if (isCounting && !timeUp)
        {
            currentTime += Time.deltaTime;

            // Update timer text every frame
            UpdateTimerDisplay();

            // Check if 5 minutes (300 seconds) reached
            if (currentTime >= levelDuration)
            {
                TimeUp();
            }
        }
    }

     private void UpdateTimerDisplay()
    {
        // Convert seconds into minutes:seconds format
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

     private void TimeUp()
    {
        // Stop the timer and player movement
        isCounting = false;
        timeUp = true;
        fPController.canMove = false;

        // Show pop-up message
        centerText.text = "";
        timeUpPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        // Reloads the current scene
            Debug.Log("Retry button pressed!");
        SceneManager.LoadScene(0);

    }



    public IEnumerator LevelCountDown()
    {
        yield return new WaitForSeconds(1f);
        centerText.text = "3";
        yield return new WaitForSeconds(1f);
        centerText.text = "2";
        yield return new WaitForSeconds(1f);
        centerText.text = "1";
        yield return new WaitForSeconds(1f);
        centerText.text = "GO!";
        yield return new WaitForSeconds(1f);
        centerText.text = "";
        fPController.canMove = true;
        isCounting = true;
    }
}
