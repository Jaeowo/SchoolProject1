using UnityEngine;

public class Capy : MonoBehaviour
{
    private bool isCollision = false;

    private Transform imageChild;
    private Transform textChild;

    private void Awake()
    {
        imageChild = transform.GetChild(0);
        textChild = transform.GetChild(1);

        ChildActiveToFalse();
    }

    void Update()
    {
        DialogueCheck();
    }

    private void ChildActiveToFalse()
    {
        imageChild.gameObject.SetActive(false);
        textChild.gameObject.SetActive(false);
    }

    private void DialogueCheck()
    {
        if (!DialogueManager.instance.isInDialogue)
        {

            if (isCollision && Input.GetKeyDown(KeyCode.Z))
            {
                if (!PlayerInfoManager.instance.GetProgress("chapter2.capy01"))
                {
                    DialogueManager.instance.StartDialogue("chapter2.capy01");
                    ChildActiveToFalse();
                }
                else if (!PlayerInfoManager.instance.GetProgress("chapter2.capy02"))
                {
                    DialogueManager.instance.StartDialogue("chapter2.capy02");
                    ChildActiveToFalse();
                }

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                DialogueManager.instance.NextDialogue();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCollision = true;

            if (!PlayerInfoManager.instance.GetProgress("chapter2.capy00"))
            {
                DialogueManager.instance.StartDialogue("chapter2.capy00");
            }
            else
            {
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
