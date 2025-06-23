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
            StartCoroutine(DelayBackPlayScene());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
        Debug.Log("FindYuzu!!!!!!");
        if (messageBubble != null)
        {
            Vector3 newPos = transform.position + new Vector3(0, 2, 0);
            messageBubble.transform.position = newPos;

            messageBubble.SetActive(true);
        }

        isColliding = false;
    }

    private IEnumerator DelayBackPlayScene()
    {
        yield return new WaitForSeconds(3f); 
        SceneManager.LoadScene("PlayScene");
    }
}