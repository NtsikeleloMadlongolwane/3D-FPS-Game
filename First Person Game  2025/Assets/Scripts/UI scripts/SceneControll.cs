using UnityEngine;
using System.Collections;
using TMPro;

public class SceneControll : MonoBehaviour
{
    [Header("Player Control")]
    public FPController fPController;
    public TextMeshProUGUI centerText;
    public MainMenuRoutines MainRoutines;
    private void Start()
    {
        fPController.canMove = false;
        StartCoroutine(MainRoutines.LoadingScreen(""));
    }
    private void Update()
    {
        if(MainRoutines.isLoading == false)
        {
            MainRoutines.isLoading = true;
            StartCoroutine(LevelCountDown());
        }
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
    }
}
