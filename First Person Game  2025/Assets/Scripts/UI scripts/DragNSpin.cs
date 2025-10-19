using UnityEngine;

public class DragNSpin : MonoBehaviour
{

    public float rotationSpeed = 5f;
    public Transform topBound;
    public Transform bottomBound;
    public Transform leftBound;
    public Transform rightBound;

    private bool isInDragArea = false;
    private bool isDragging = false;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {

        Vector3 mousePos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0) && isInDragArea)
        {
            isDragging = true;
        }

        // Stop dragging if mouse goes outside bounds or button is released
        if (!Input.GetMouseButton(0) || !isInDragArea)
        {
            isDragging = false;
        }

        if (isDragging)
        {
            float mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up, -mouseX * rotationSpeed, Space.World);
        }

        
        Vector3 top = Camera.main.WorldToScreenPoint(topBound.position);
        Vector3 bottom = Camera.main.WorldToScreenPoint(bottomBound.position);
        Vector3 left = Camera.main.WorldToScreenPoint(leftBound.position);
        Vector3 right = Camera.main.WorldToScreenPoint(rightBound.position);

        if ((mousePos.y < top.y && mousePos.y > bottom.y)
            && (mousePos.x < right.x && mousePos.x > left.x))
        {
            isInDragArea = true;
        }
        else
        {
            isInDragArea = false;
        }
    }
}
