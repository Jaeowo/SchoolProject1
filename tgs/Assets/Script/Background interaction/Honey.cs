using UnityEngine;
using UnityEngine.SceneManagement;

public class Honey : MonoBehaviour
{
    private bool isCollision = false;

    private Transform imageChild;
    private Transform textChild;

    private const float sceneChangeTime = 1.0f;
    private float timer = 0.0f;
    private bool timerTrigger = false;

    public GameObject player;

    void Awake()
    {
        imageChild = transform.GetChild(0);
        textChild = transform.GetChild(1);

        ChildActiveToFalse();

        //Test
        //PlayerInfoManager.instance.SetProgress("chapter3.kuma0", true);
    }
    void Update()
    {
        if (!PlayerInfoManager.instance.GetProgress("CollectHoney")&&
            PlayerInfoManager.instance.GetProgress("chapter3.kuma0"))
        {
            if (timerTrigger)
            {
                timer += Time.deltaTime;
            }

            if (isCollision && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton1)))
            {
                timerTrigger = true;
                CameraManager.instance.FadeOut();
            }

            if (timer >= sceneChangeTime)
            {
                Vector3 playerPos = player.transform.position;
                PlayerInfoManager.instance.SavePlayerPosition(playerPos);

                SceneManager.LoadScene("HoneyScene");

            }
        }
    }

    private void ChildActiveToFalse()
    {
        imageChild.gameObject.SetActive(false);
        textChild.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PlayerInfoManager.instance.GetProgress("CollectHoney"))
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
}
