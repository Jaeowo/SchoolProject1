using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HoneySceneManager : MonoBehaviour
{
    public static HoneySceneManager instance { get; private set; }

    public GameObject messageBubble;

    public int gettingHoney { get; set; } = 0;
    public int hp { get; set; } = 3;

    private bool hasSceneReturned = false;

    // UI Connect
    public GameObject[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public TextMeshProUGUI honeyText;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Update()
    {
        HeartCheck();

        if (!hasSceneReturned && gettingHoney >= 3)
        {
            PlayerInfoManager.instance.SetProgress("CollectHoney", true);
            StartCoroutine(DelayedReaction());
            StartCoroutine(DelayBackPlayScene());
        }

        honeyText.text = gettingHoney.ToString() + "/3";
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

    private void HeartCheck()
    {
        if(hp <=0)
        {
            hp = 0;
        }

        for(int i=0; i< hearts.Length; i++)
        {
            var img = hearts[i].GetComponent<Image>();
            if (img != null)
            {
                img.sprite = i < hp ? fullHeart : emptyHeart;
            }
        }
    }
}

