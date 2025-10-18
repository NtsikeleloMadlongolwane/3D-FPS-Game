using UnityEngine;

public class PopUpAnimation : MonoBehaviour
{

    public Vector3 targetScale = new Vector3(1f, 1f, 1f);
    public Vector3 startingScale = new Vector3(0.5f, 0.5f, 0.5f);
    public float xSpeed = 2f;
    public float ySpeed = 1f;

    private bool animateX = false;
    private bool animateY = false;

    void OnEnable()
    {
        transform.localScale = startingScale;
        animateX = true;
        animateY = false;
        Debug.Log("Animation stared");
    }

    void Update()
    {
        if (animateX)
        {
            float newX = Mathf.MoveTowards(transform.localScale.x, targetScale.x, xSpeed * Time.deltaTime);
            transform.localScale = new Vector3(newX, startingScale.y, 1f);

            if (Mathf.Approximately(newX, targetScale.x))
            {
                animateX = false;
                animateY = true;
            }
        }
        else if (animateY)
        {
            float newY = Mathf.MoveTowards(transform.localScale.y, targetScale.y, ySpeed * Time.deltaTime);
            transform.localScale = new Vector3(targetScale.x, newY, 1f);

            if (Mathf.Approximately(newY, targetScale.y))
            {
                animateY = false;
                Debug.Log("Animation done");
            }
        }
    }

}
