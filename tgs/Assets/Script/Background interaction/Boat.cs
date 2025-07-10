using UnityEngine;
using UnityEngine.SceneManagement;

public class Boat : MonoBehaviour
{
    private bool isCollision = false;

    private Transform imageChild;
    private Transform textChild;

    private const float sceneChangeTime = 1.0f;
    private float timer = 0.0f;
    private bool timerTrigger = false;

    void Start()
    {
        imageChild = transform.GetChild(0);
        textChild = transform.GetChild(1);

        ChildActiveToFalse();
    }

    void Update()
    {
        if (timerTrigger)
        {
            timer += Time.deltaTime;
        }

        if (isCollision && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton1)))
        {
            timerTrigger = true;
            CameraManager.instance.FadeOut();
            SceneManager.LoadScene("RiverScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerInfoManager.instance.GetProgress("chapter4.useTicket"))
        {
            if (collision.CompareTag("Player"))
            {
                isCollision = true;

                imageChild.gameObject.SetActive(true);
                textChild.gameObject.SetActive(true);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCollision = false;

            ChildActiveToFalse();
        }
    }

    private void ChildActiveToFalse()
    {
        imageChild.gameObject.SetActive(false);
        textChild.gameObject.SetActive(false);
    }
}
