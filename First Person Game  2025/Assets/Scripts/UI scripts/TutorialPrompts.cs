using UnityEngine;
using System.Collections;
using TMPro;
public class TutorialPrompts : MonoBehaviour
{
    public TextMeshProUGUI centerTextInfo;
    public float promptDuration = 2f;
    public string Instruction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Instructions(Instruction));
        }
    }
    private IEnumerator Instructions(string prompt)
    {
        if(centerTextInfo!= null)
        {
            centerTextInfo.text = prompt;
            yield return new WaitForSeconds(promptDuration);
            centerTextInfo.text = "";
        }
    }
}
