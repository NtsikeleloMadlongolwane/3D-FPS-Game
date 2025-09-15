using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManageemment : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Ntsikelelo");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
