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
    public bool isLoading = true;
    public GameObject loadingscreen;
    public float maxLoadTime = 5f;
    public TMP_Text tipsTMPro;
    public string[] tipsList;

    [Header("Generating")]
    public GameObject generatingPrompt;
    public TMP_Text generatingText;
    public float generatingTime = 2f; 

    [Header("Main Menu Screen")]
    public GameObject MainMenuScreen;
    public bool isMainMenu = false;

    [Header("Main Menu Button Press Screens")]
    public GameObject selectLevel;
    public GameObject controls;
    public GameObject playerStats;
    public GameObject quitScreen;

    [Header("In-Game Menus")]
    public FPController fPController;
    public GameObject HUD;
    public GameObject pauseMenu;
    public GameObject inGameControls;
    public GameObject restartPrompt;
    public GameObject onGameQuit;
    private void Start()
    {
        if (isMainMenu)
        {
                GameScreenClick();
        }
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
    public System.Collections.IEnumerator LoadingScreen(string sceneName)
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
        if(sceneName == "MainMenu")
        {
            MainMenuClick();
        }
        else
        {

        }
        isLoading = false;
    }

    // button clicks
    public void GameScreenClick()
    {
        StartCoroutine(GameName());
    }
    public void EnterGameClick()
    {
        StartCoroutine(LoadingScreen("MainMenu"));    
    } 
    public void MainMenuClick()
    {
        MainMenuScreen.SetActive(true);
    }

    // main menu buttons
    public void levelSelect()
    {
        selectLevel.SetActive(true);
        // set off
        controls.SetActive(false);
        playerStats.SetActive(false);
        quitScreen.SetActive(false);
    }
            public void Level_Tut()
            {
                StartCoroutine(Generating("Tutorial Level"));
            }
            public void Level_One()
            {
                StartCoroutine(Generating("Level 1"));
            }
    public void ControlsMain()
    {
        controls.SetActive(true);
        // set off
        selectLevel.SetActive(false);
        playerStats.SetActive(false);
        quitScreen.SetActive(false);
    }
    public void PlayerStats()
    {
        playerStats.SetActive(true);
        // set off
        selectLevel.SetActive(false);
        controls.SetActive(false);
        quitScreen.SetActive(false);
    }
    public void QuitGamePrompt()
    {
        quitScreen.SetActive(true);
        // set off
        selectLevel.SetActive(false);
        controls.SetActive(false);
        playerStats.SetActive(false);
    }

    System.Collections.IEnumerator Generating(string levelName)
    {
        generatingPrompt.SetActive(true);

        if (isMainMenu)
        {
            generatingText.text = "GENERATING LEVEL";
        }
        else
        {
            generatingText.text = "REGENERATING LEVEL";
        }
        yield return new WaitForSeconds(generatingTime);
        generatingText.text = "LEVEL READY";
        yield return new WaitForSeconds(1F);

        SceneManager.LoadScene(levelName);
    }
    
    // in game menus

    public void PauseMenu()
    {
        if(fPController.isPaused == true)
        {
            pauseMenu.SetActive(true);
            HUD.SetActive(false);

            inGameControls.SetActive(false);
            onGameQuit.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(false);
            HUD.SetActive(true);
        }
    }

    public void InGameControls()
    {
        inGameControls.SetActive(true);
        onGameQuit.SetActive(false);
        restartPrompt.SetActive(false);
    }
    public void ReStart()
    {
        inGameControls.SetActive(false);
        onGameQuit.SetActive(false);
        restartPrompt.SetActive(true);
    }

    public void Quit2Main()
    {
        inGameControls.SetActive(false);
        onGameQuit.SetActive(true);
        restartPrompt.SetActive(false);
    }

    public void ClearMainMenu()
    {
        selectLevel.SetActive(false);
        controls.SetActive(false);
        playerStats.SetActive(false);
        quitScreen.SetActive(false);
    }

    public void ClearPauseMenu()
    {
        inGameControls.SetActive(false);
        restartPrompt.SetActive(false);
        onGameQuit.SetActive(false);
    }
}

