using UnityEngine;

public class HoneyComb : MonoBehaviour
{
    private bool isColliding = false;

    private Transform imageChild;
    private Transform textChild;

    void Start()
    {
        imageChild = transform.GetChild(0);
        textChild = transform.GetChild(1);

        ChildActiveToFalse();
    }

    void Update()
    {
        if (isColliding)
        {
            if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                HoneySceneManager.instance.gettingHoney += 1;
                Destroy(gameObject);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = true;

            imageChild.gameObject.SetActive(true);
            textChild.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isColliding = false;

            ChildActiveToFalse();
        }
    }

    private void ChildActiveToFalse()
    {
        imageChild.gameObject.SetActive(false);
        textChild.gameObject.SetActive(false);
    }
}
