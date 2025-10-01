using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuRoutines : MonoBehaviour
{
    [Header("Game Name Screen Settings")]
    public GameObject gameNameScreen;
    public GameObject enterGameButton;
    public float startButtonTimer = 10f;

    [Header("Loading Screen Settings")]
    public GameObject loadingscreen;
    public float maxLoadTime = 5f;
    public TMP_Text tipsTMPro;
    public string[] tipsList;

    [Header("Main Menu Screen")]
    public GameObject MainMenuScreen;

    private void Start()
    {
        GameScreenClick();
    }

    // courituines
    System.Collections.IEnumerator GameName()
    {
        // set other screen inactive
        loadingscreen.SetActive(false);

        // set screen active
        gameNameScreen.SetActive(true);

        yield return new WaitForSeconds(startButtonTimer);
        enterGameButton.SetActive(true);
    }
    System.Collections.IEnumerator LoadingScreen()
    {
        // set other screen inactive
        gameNameScreen.SetActive(false);

        // set screen active
        loadingscreen.SetActive(true);

        float ScreenLength = Random.Range(5f, maxLoadTime);
        int randomTip = Random.Range(0, tipsList.Length);
        tipsTMPro.text = tipsList[randomTip];
      
        yield return new WaitForSeconds(ScreenLength);
        loadingscreen.SetActive(false);
    }

    // button clicks
    public void GameScreenClick()
    {
        StartCoroutine(GameName());
    }
    public void EnterGameClick()
    {
        StartCoroutine(LoadingScreen());
        MainMenuClick();
    } 
    public void MainMenuClick()
    {
        MainMenuScreen.SetActive(true);
    }
}
