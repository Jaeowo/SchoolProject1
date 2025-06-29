using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HoneySceneManager : MonoBehaviour
{
    public static HoneySceneManager instance { get; private set; }

    public GameObject messageBubble;
    public GameObject gameOverPanel;
    public GameObject blurImage;
    public GameObject player;

    public int gettingHoney { get; set; } = 0;
    public int hp { get; set; } = 3;
    public bool isOver { get; set; } = false;

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

    private void Start()
    {
        messageBubble.SetActive(false);
        gameOverPanel.SetActive(false);
        blurImage.SetActive(false);
    }

    void Update()
    {
        HeartCheck();
        GameOver();
        GameClear();

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
            Vector3 newPos = player.transform.position + new Vector3(0, 2, 0);
            messageBubble.transform.position = newPos;

            messageBubble.SetActive(true);
        }
    }

    private void HeartCheck()
    {
        if( hp <=0 )
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

    private void GameOver()
    {
        if(hp <= 0 )
        {
            isOver = true;
            gameOverPanel.SetActive(true);
            blurImage.SetActive(true);
        }
    }

    private void GameClear()
    {
        if (!hasSceneReturned && gettingHoney >= 3)
        {
            isOver = true;
            PlayerInfoManager.instance.SetProgress("CollectHoney", true);
            StartCoroutine(DelayedReaction());
            StartCoroutine(DelayBackPlayScene());
        }
    }

    public void GameRestart()
    {
        SceneManager.LoadScene("HoneyScene");
    }
}

