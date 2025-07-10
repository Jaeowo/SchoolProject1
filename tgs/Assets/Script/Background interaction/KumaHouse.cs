using System.Collections;
using UnityEngine;

public class KumaHouse : MonoBehaviour
{
    public GameObject player;
    public GameObject password;

    private bool isCollision = false;

    private Transform imageChild;
    private Transform textChild;
    private Transform playerTransform;

    void Start()
    {
        password.SetActive(false);

        imageChild = transform.GetChild(0);
        textChild = transform.GetChild(1);

        ChildActiveToFalse();

        playerTransform = player.GetComponent<Transform>();

        // Test
        // Later Please Delete here
        PlayerInfoManager.instance.SetProgress("chapter2.capy04", true);
    }

    void Update()
    {
        //if (!DialogueManager.instance.isInDialogue)
        //{
        //    if (isCollision && PlayerInfoManager.instance.GetProgress("chapter2.capy04"))
        //    {
        //        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton1))
        //        {
        //            if (!PlayerInfoManager.instance.GetProgress("chapter3.password"))
        //            {
        //                DialogueManager.instance.StartDialogue("chapter3.password");
        //            }
        //            else if (isPass)
        //            {
        //                player.transform.position = new Vector3(107f, -5.8f, 0f);
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton1))
        //    {
        //        DialogueManager.instance.NextDialogue();
        //    }
        //}
        if (isCollision && PlayerInfoManager.instance.GetProgress("chapter2.capy04"))
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                if (!PlayerInfoManager.instance.GetProgress("PassKumaHouse"))
                {
                    password.SetActive(true);
                    PlayerInfoManager.instance.SetIsMoving(false);
                }
                else
                {
                    PlayerInfoManager.instance.SavePlayerPosition(playerTransform.position);
                    StartCoroutine(FadeOutInAndMove());
                }
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

    IEnumerator FadeOutInAndMove()
    {

        CameraManager.instance.FadeOut();

        yield return new WaitForSeconds(CameraManager.instance.fadeDuration);
        player.transform.position = new Vector3(107f, -5.8f, 0f);
        yield return new WaitForSeconds(1.5f);

        CameraManager.instance.FadeIn();
        PlayerInfoManager.instance.SetIsMoving(true);
    }
}
