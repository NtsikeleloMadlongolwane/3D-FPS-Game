using UnityEngine;

public class MousePision : MonoBehaviour
{
    [SerializeField]
   // private bool isInDragArea = false;
    private Vector3 mousePosition;

    public Transform topBound;
    public Transform bottomBound;
    public Transform leftBound;
    public Transform rightBound;
    void Update()
    {
        Vector3 top = Camera.main.WorldToScreenPoint(topBound.position);
        Vector3 bottom = Camera.main.WorldToScreenPoint(bottomBound.position);
        Vector3 left = Camera.main.WorldToScreenPoint(leftBound.position);
        Vector3 right = Camera.main.WorldToScreenPoint(rightBound.position);

        mousePosition = Input.mousePosition;
        if ((mousePosition.y < top.y && mousePosition.y > bottom.y) 
            && (mousePosition.x < right.x && mousePosition.x > left.x))
        {
            //isInDragArea = true;
        }
        else
        {
            //isInDragArea = false;
        }
    }
}
