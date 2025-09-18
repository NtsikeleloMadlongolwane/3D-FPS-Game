using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public void GoToLevelOne()
    {
        SceneManager.LoadScene("Level 1");
        Debug.Log("switched");
    }
}
