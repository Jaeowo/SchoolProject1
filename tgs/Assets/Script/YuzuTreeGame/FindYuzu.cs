using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FindYuzu : MonoBehaviour
{
    public GameObject yuzu;
    public GameObject messageBubble;

    private bool isColliding = false;
    private bool hasSceneReturned = false;

    private void Update()
    {
        if (!hasSceneReturned && PlayerInfoManager.instance.GetProgress("FindYuzu"))
        {
            hasSceneReturned = true;
            StartCoroutine(DelayBackPlaySceneAsync());
        }

        //test
        if (Input.GetKeyDown(KeyCode.Q))
        {

            PlayerInfoManager.instance.SetProgress("FindYuzu", true);
            StartCoroutine(DelayedReaction());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isColliding && collision.gameObject == yuzu)
        {
            isColliding = true;
            PlayerInfoManager.instance.SetProgress("FindYuzu", true);
            StartCoroutine(DelayedReaction());
        }
    }

    private IEnumerator DelayedReaction()
    {
        yield return new WaitForSeconds(0.5f);
        if (messageBubble != null)
        {
            Vector3 newPos = transform.position + new Vector3(0, 3, 0);
            messageBubble.transform.position = newPos;
            messageBubble.SetActive(true);
        }

        isColliding = false;
    }

    private IEnumerator DelayBackPlaySceneAsync()
    {
        yield return new WaitForSeconds(3f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("PlayScene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
