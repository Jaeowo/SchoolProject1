using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance { get; private set; }

    public GameObject player;
    private Transform playerTransform;

    public Image fadeImage;
    public float fadeDuration = 1.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            playerTransform = player.transform;
        }
  
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "PlayScene")
        {
            if (DialogueManager.instance != null && DialogueManager.instance.isInDialogue)
            {

                var currentDialogue = DialogueManager.instance.CurrentDialogue;
                if (currentDialogue != null)
                {

                    Speeker speekerData = DialogueManager.instance.allSpeekerList.Find(s => s.name == currentDialogue.character);
                    if (speekerData != null && speekerData.character != null)
                    {
                        Vector3 targetPos = speekerData.character.transform.position;
                        targetPos.z = -10f;
                        transform.position = Vector3.Lerp(transform.position, targetPos, 2f * Time.deltaTime);
                        return;
                    }
                }
            }

            Vector3 playerPos = playerTransform.position;
            Vector3 newPlayerCameraPos = new Vector3(playerPos.x, playerPos.y + 2.5f, -10f);
            transform.position = Vector3.Lerp(transform.position, newPlayerCameraPos, 2f * Time.deltaTime);
        }

        if (SceneManager.GetActiveScene().name == "YuzuTreeScene")
        { 
            Vector3 playerPos = player.transform.position;

            float fixedX = 0f; 
            Vector3 cameraTargetPos = new Vector3(fixedX, playerPos.y + 2.5f, -10f);

            transform.position = Vector3.Lerp(transform.position, cameraTargetPos, 2f * Time.deltaTime);
        }



    }

    //Black -> Clear
    public void FadeIn()
    {
        StartCoroutine(Fade(1f, 0f));
    }

    //Clear -> Black
    public void FadeOut()
    {
        StartCoroutine(Fade(0f, 1f));
    }

    private IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        Color c = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            c.a = Mathf.Lerp(from, to, t);
            fadeImage.color = c;
            yield return null;
        }

        c.a = to;
        fadeImage.color = c;
    }
}
