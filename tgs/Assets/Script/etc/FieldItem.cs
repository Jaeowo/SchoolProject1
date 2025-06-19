using TMPro;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    public string itemID;

    private Transform imageChild;
    private Transform textChild;

    private bool isCollision = false;

    private void Awake()
    {
        imageChild = transform.GetChild(0);
        textChild = transform.GetChild(1);

        imageChild.gameObject.SetActive(false);
        textChild.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(isCollision && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton1)))
        {
            ItemManager.instance.GetItem(itemID);
            Destroy(gameObject);
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

            imageChild.gameObject.SetActive(false);
            textChild.gameObject.SetActive(false);
        }
    }

}
