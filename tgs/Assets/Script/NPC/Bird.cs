using UnityEngine;

public class Bird : MonoBehaviour
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

    // Update is called once per frame
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

            if (isCollision && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton1)))
            {
                if (!PlayerInfoManager.instance.GetProgress("chapter1.bird01"))
                {
                    DialogueManager.instance.StartDialogue("chapter1.bird01");

                    ChildActiveToFalse();

                }

                if (PlayerInfoManager.instance.GetProgress("chapter1.bird01"))
                {
                    if (ItemManager.instance.FindItem("Branch"))
                    {
                        DialogueManager.instance.StartDialogue("chapter1.bird03");
                        ItemManager.instance.RemoveItem("Branch");
                        ChildActiveToFalse();

                    }
                    else if (!PlayerInfoManager.instance.GetProgress("chapter1.bird03"))
                    {
                        DialogueManager.instance.StartDialogue("chapter1.bird02");
                        ChildActiveToFalse();
                    }
                }

                if (PlayerInfoManager.instance.GetProgress("chapter1.bird03"))
                {
                    DialogueManager.instance.StartDialogue("chapter1.bird04");
                }

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton1))
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
}
