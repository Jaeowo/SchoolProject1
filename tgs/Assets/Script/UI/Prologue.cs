using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Prologue : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public Image image3;

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;

    private int progress = 0;
    private bool isTransitioning = false;

    private bool[] stepCompleted = new bool[7];

    void Start()
    {
        if(!PlayerInfoManager.instance.GetProgress("Prologue"))
        {
            SetAlpha(image1, 0);
            SetAlpha(image2, 0);
            SetAlpha(image3, 0);
            SetAlpha(text1, 0);
            SetAlpha(text2, 0);
            SetAlpha(text3, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isTransitioning) return;

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            switch (progress)
            {
                case 0:
                    StartCoroutine(FadeIn(image1, 0));
                    progress++;
                    break;
                case 1:
                    if (stepCompleted[0]) { StartCoroutine(FadeIn(text1, 1)); progress++; }
                    break;
                case 2:
                    if (stepCompleted[1]) { StartCoroutine(FadeIn(image2, 2)); progress++; }
                    break;
                case 3:
                    if (stepCompleted[2]) { StartCoroutine(FadeIn(text2, 3)); progress++; }
                    break;
                case 4:
                    if (stepCompleted[3]) { StartCoroutine(FadeOutMultipleAndThenShowFinal()); progress++; }
                    break;
            }
        }
    }

    private void SetAlpha(Graphic g, float alpha)
    {
        Color c = g.color;
        c.a = alpha;
        g.color = c;
    }

    private IEnumerator FadeIn(Graphic g, int stepIndex, float duration = 1f)
    {
        isTransitioning = true;
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            SetAlpha(g, Mathf.Clamp01(time / duration));
            yield return null;
        }
        stepCompleted[stepIndex] = true; // 이 단계 완료됨
        isTransitioning = false;
    }

    private IEnumerator FadeOutMultipleAndThenShowFinal()
    {
        isTransitioning = true;
        float duration = 1.5f;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - time / duration);
            SetAlpha(image1, alpha);
            SetAlpha(image2, alpha);
            SetAlpha(text1, alpha);
            SetAlpha(text2, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(FadeIn(image3, 5));  
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(FadeInAndDestroy(text3, 6)); 
    }

    private IEnumerator FadeInAndDestroy(Graphic g, int stepIndex, float duration = 1f)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            SetAlpha(g, Mathf.Clamp01(time / duration));
            yield return null;
        }
        stepCompleted[stepIndex] = true;

        yield return new WaitForSeconds(5f);

        PlayerInfoManager.instance.SetProgress("Prologue", true);

        Destroy(gameObject);
    }
}
