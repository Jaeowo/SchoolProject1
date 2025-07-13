using UnityEngine;

public class Kuma : MonoBehaviour
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
                if (!PlayerInfoManager.instance.GetProgress("chapter3.kuma0"))
                {
                    DialogueManager.instance.StartDialogue("chapter3.kuma0");

                    ChildActiveToFalse();

                }

                if (PlayerInfoManager.instance.GetProgress("chapter3.kuma0"))
                {
                    if (ItemManager.instance.FindItem("Honey"))
                    {
                        DialogueManager.instance.StartDialogue("chapter3.kuma1");
                        ItemManager.instance.RemoveItem("Honey");
                        ItemManager.instance.GetItem("Ticket");
                        ChildActiveToFalse();

                    }
            
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
