using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManageemment : MonoBehaviour
{
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial Level");
        Debug.Log("Loaded Ntsikeleo Scene");
    }
    public void LevelOne()
    {
        SceneManager.LoadScene("Level 1");
        Debug.Log("Loaded Level 1 Scene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
