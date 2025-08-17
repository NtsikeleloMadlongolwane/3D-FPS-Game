using UnityEngine;

public class Marking : MonoBehaviour
{
    public Material markedMaterial;
    public Material unmarkedMaterial;

    public Material currentMaterial;
    public void Marked()
    {
        currentMaterial = markedMaterial;
        SetMatrial();
    }
    public void Unmarked()
    {
        currentMaterial = unmarkedMaterial;
        SetMatrial();
    }

    public void SetMatrial()
    {
        gameObject.GetComponent<Renderer>().material = currentMaterial;
    }

}
