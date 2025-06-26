using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoneySceneManager : MonoBehaviour
{
    public static HoneySceneManager instance { get; private set; }

    public GameObject messageBubble;

    public int GettingHoney { get; set; } = 0;
    public int HP { get; set; } = 3;

    private bool hasSceneReturned = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (!hasSceneReturned && GettingHoney >=3)
        {
            PlayerInfoManager.instance.SetProgress("CollectHoney", true);
            StartCoroutine(DelayedReaction());
            StartCoroutine(DelayBackPlayScene());
        }
    }

    private IEnumerator DelayBackPlayScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("PlayScene");
    }

    private IEnumerator DelayedReaction()
    {
        yield return new WaitForSeconds(0.5f);
        if (messageBubble != null)
        {
            Vector3 newPos = transform.position + new Vector3(0, 2, 0);
            messageBubble.transform.position = newPos;

            messageBubble.SetActive(true);
        }
    }
}
