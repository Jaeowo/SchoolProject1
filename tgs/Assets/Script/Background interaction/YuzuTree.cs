using UnityEngine;
using UnityEngine.SceneManagement;

public class YuzuTree : MonoBehaviour
{
    private bool isCollision = false;

    private Transform imageChild;
    private Transform textChild;

    private const float sceneChangeTime = 1.0f;
    private float timer = 0.0f;
    private bool timerTrigger = false;

    public GameObject player;

    void Start()
    {
        imageChild = transform.GetChild(0);
        textChild = transform.GetChild(1);

        ChildActiveToFalse();
    }

    void Update()
    {
        if (!PlayerInfoManager.instance.GetProgress("FindYuzu"))
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

                SceneManager.LoadScene("YuzuTreeScene");
            }
        }
 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PlayerInfoManager.instance.GetProgress("FindYuzu"))
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
