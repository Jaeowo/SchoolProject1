using System.Collections;
using UnityEngine;

public class KumaHouseExit : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransform;

    private bool isCollision = false;

    private Transform imageChild;
    private Transform textChild;

    void Start()
    {
        imageChild = transform.GetChild(0);
        textChild = transform.GetChild(1);

        ChildActiveToFalse();

        playerTransform = player.GetComponent<Transform>();
    }

    void Update()
    {
        if (isCollision && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton1)))
        {
            StartCoroutine(FadeOutInAndMove());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCollision = true;

            imageChild.gameObject.SetActive(true);
            textChild.gameObject.SetActive(true);
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

    IEnumerator FadeOutInAndMove()
    {

        CameraManager.instance.FadeOut();

        yield return new WaitForSeconds(CameraManager.instance.fadeDuration);
        playerTransform.position = PlayerInfoManager.instance.LoadPlayerPosition();
        yield return new WaitForSeconds(1.5f);

        CameraManager.instance.FadeIn();
        PlayerInfoManager.instance.SetIsMoving(true);
    }
}
