using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraManagerYuzuTree : MonoBehaviour
{
    public GameObject player;      
    public float yOffset = 2.5f;   
    public float followSpeed = 2f; 
    public float fixedX = 0f;  

    private Transform playerTransform;

    public Image fadeImage;
    public float fadeDuration = 1.0f;

    void Start()
    {
        if (player != null)
            playerTransform = player.transform;
    }

    void Update()
    {
        if (playerTransform == null)
            return;

        Vector3 targetPos = new Vector3(fixedX, playerTransform.position.y + yOffset, -10f);

        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
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
