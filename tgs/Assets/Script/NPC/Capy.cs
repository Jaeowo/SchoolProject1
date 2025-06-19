using UnityEngine;

public class Capy : MonoBehaviour
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

    void Update()
    {

        if (PlayerInfoManager.instance.GetProgress("chapter2.capy00"))
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
                if (!PlayerInfoManager.instance.GetProgress("chapter2.capy01"))
                {
                    DialogueManager.instance.StartDialogue("chapter2.capy01");
                    ChildActiveToFalse();
                }

                if (PlayerInfoManager.instance.GetProgress("chapter2.capy01"))
                {
                    if (ItemManager.instance.FindItem("Yuzu"))
                    {
                        DialogueManager.instance.StartDialogue("chapter2.capy03");
                        ItemManager.instance.RemoveItem("Yuzu");
                        ChildActiveToFalse();

                    }
                    else if (!PlayerInfoManager.instance.GetProgress("chapter2.capy03"))
                    {
                        //DialogueManager.instance.StartDialogue("chapter2.capy02");
                        //ChildActiveToFalse();


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
            inputCooldownTimer = inputCooldown;

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
