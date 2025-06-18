using UnityEngine;
using UnityEngine.SceneManagement;

public class YuzuTree : MonoBehaviour
{
    private bool isCollision = false;

    private Transform imageChild;
    private Transform textChild;

    void Start()
    {
        imageChild = transform.GetChild(0);
        textChild = transform.GetChild(1);

        ChildActiveToFalse();
    }

    void Update()
    {
        if (isCollision && Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("YuzuTreeScene");
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
}
