using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RiverSceneManager : MonoBehaviour
{
    public static RiverSceneManager instance { get; private set; }

    //private bool hasSceneReturned = false;

    // Stat
    public int hp { get; set; } = 3;
    public bool isOver { get; set; } = false;

    // UI Connect
    public GameObject gameOverPanel;
    public GameObject blurImage;

    public GameObject[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Prefab
    public GameObject obstaclePrefab;
    private int clearCheck = 0;
    private float frequncy = 1.8f;
    private float spawnTimer = 0f;

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
        gameOverPanel.SetActive(false);
        blurImage.SetActive(false);
    }

    private void Update()
    {
        GameOver();
        GameClear();

        SpawnObstacle();

        HeartCheck();       
    }

    private void HeartCheck()
    {
        if (hp <= 0)
        {
            hp = 0;
        }

        for (int i = 0; i < hearts.Length; i++)
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
        if (hp <= 0)
        {
            isOver = true;
            gameOverPanel.SetActive(true);
            blurImage.SetActive(true);
        }
    }

    private void GameClear()
    {
        if (clearCheck >= 20)
        {
            //PlayerInfoManager.instance.SetProgress("EndBoat", true);
            isOver = true;
            // Find another stage
        }
    }

    public void GameRestart()
    {
        SceneManager.LoadScene("RiverScene");
    }

    private void SpawnObstacle()
    {
        if (!isOver)
        {
            if (spawnTimer < frequncy)
            {
                spawnTimer += Time.deltaTime;
            }
            else
            {
                Instantiate(obstaclePrefab);
                Debug.Log("Spawn");
                spawnTimer = 0f;
                clearCheck++;
                Debug.Log("clear :" + clearCheck);
            }
        }

    }
}
