using UnityEngine;

public class PlatformSwitch : MonoBehaviour
{
    public int switchType = 1;

    public GameObject platform;
    public int numberOfHits = 0;
    public int maximumHits = 5;
    public Transform startPosition;
    public Transform endPosistion;

    public bool movingToTop = true;
    public float moveSpeed = 10f;

    public void Update()
    {
        if (movingToTop)
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, startPosition.position, moveSpeed * Time.deltaTime);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
          if(numberOfHits != maximumHits)
            {
                movingToTop = false;

                Vector3 platformIntevals = (startPosition.transform.position - endPosistion.transform.position) / 5f;
                platform.transform.position = platform.transform.position - platformIntevals;
                Debug.Log(platform.transform.position);
                numberOfHits++;

            }
            else
            {
                numberOfHits = 0;
                movingToTop = true;
            }
        }
    }

}
