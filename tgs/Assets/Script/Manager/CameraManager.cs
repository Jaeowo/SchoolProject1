using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
        playerTransform = player.transform;
    }

    void Update()
    {

        if (DialogueManager.instance != null && DialogueManager.instance.isInDialogue)
            return;

        Vector3 targetPos = new Vector3(playerTransform.position.x, playerTransform.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, targetPos, 2f * Time.deltaTime);


        //Test
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    FadeOut();
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    FadeIn();
        //}
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
