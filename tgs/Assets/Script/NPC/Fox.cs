using UnityEngine;

public class Fox : MonoBehaviour
{
    private bool isCollision = false;

    private Transform imageChild;
    private Transform textChild;


    private float inputCooldown = 0.5f;
    private float inputCooldownTimer = 0f;

    private void Awake()
    {
        imageChild = transform.GetChild(0);
        textChild = transform.GetChild(1);

        ChildActiveToFalse();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInfoManager.instance.GetProgress("chapter4.greeting"))
        {
            if (inputCooldownTimer > 0)
            {
                inputCooldownTimer -= Time.deltaTime;

                if (inputCooldownTimer <= 0f)
                {
                    inputCooldownTimer = 0f;

                    if (isCollision)
                    {
                        imageChild.gameObject.SetActive(true);
                        textChild.gameObject.SetActive(true);
                    }
                }
                return;
            }
            DialogueCheck();
        }
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
                if (PlayerInfoManager.instance.GetProgress("chapter4.greeting") &&
                    !PlayerInfoManager.instance.GetProgress("chapter4.haventTicket") &&
                    !PlayerInfoManager.instance.GetProgress("chapter4.useTicket") &&
                    !ItemManager.instance.FindItem("Ticket"))
                {
                    DialogueManager.instance.StartDialogue("chapter4.haventTicket");
                    ChildActiveToFalse();
                    return;
                }

                if (PlayerInfoManager.instance.GetProgress("chapter4.greeting") &&
                    !PlayerInfoManager.instance.GetProgress("chapter4.useTicket") &&
                    ItemManager.instance.FindItem("Ticket"))
                {
                    DialogueManager.instance.StartDialogue("chapter4.useTicket");
                    ItemManager.instance.RemoveItem("Ticket");
                    ChildActiveToFalse();
                    return;
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
            inputCooldownTimer = inputCooldown;

            if (!PlayerInfoManager.instance.GetProgress("chapter4.greeting"))
            {
                DialogueManager.instance.StartDialogue("chapter4.greeting");
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
