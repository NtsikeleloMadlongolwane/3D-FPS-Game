using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public int sceneBuildIndex = 1;

    // Public method so it can be hooked to a Button OnClick()
    public void PlayGame()
    {
        // Simple synchronous load by build index
        SceneManager.LoadScene(sceneBuildIndex);
    }

        public void QuitGame()
    {
        // Save preferences, player data, etc. here if needed
        Debug.Log("Quit requested");

        // Quit in a real build
        Application.Quit();

        // Stop play mode in the Editor (so you can test)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
