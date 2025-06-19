using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class FindYuzu : MonoBehaviour
{
    public GameObject yuzu;
    public GameObject messageBubble;

    private bool isColliding = false;

    private void Update()
    {
        if (PlayerInfoManager.instance.GetProgress("FindYuzu"))
        {
            StartCoroutine(DelayBackPlayScene());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isColliding && collision.gameObject == yuzu)
        {
            isColliding = true;
            StartCoroutine(DelayedReaction());
        }
    }

    private IEnumerator DelayedReaction()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("FindYuzu!!!!!!");
        messageBubble.SetActive(true);
        PlayerInfoManager.instance.SetProgress("FindYuzu", true);

        isColliding = false;
    }

    private IEnumerator DelayBackPlayScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("PlayScene");
    }
}
