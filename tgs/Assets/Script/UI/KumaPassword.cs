using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KumaPassword : MonoBehaviour
{
    public Button incorrect1;
    public Button incorrect2;
    public Button correct;

    public GameObject player;

    public GameObject passwordPanel;

    private void Start()
    {
        incorrect1.onClick.AddListener(OnIncorrectButtonClick);
        incorrect2.onClick.AddListener(OnIncorrectButtonClick);
        correct.onClick.AddListener(OnCorrectButtonClick);

        passwordPanel.SetActive(false);
    }

    void OnIncorrectButtonClick()
    {
        passwordPanel.SetActive(false);
        PlayerInfoManager.instance.SetIsMoving(true);
    }

    void OnCorrectButtonClick()
    {
        PlayerInfoManager.instance.SetProgress("PassKumaHouse", true);
        passwordPanel.SetActive(false);
        StartCoroutine(FadeOutInAndMove());
    }


    IEnumerator FadeOutInAndMove()
    {

        CameraManager.instance.FadeOut();

        yield return new WaitForSeconds(CameraManager.instance.fadeDuration);
        player.transform.position = new Vector3(107f, -5.8f, 0f);
        yield return new WaitForSeconds(1.5f);

        CameraManager.instance.FadeIn();
        PlayerInfoManager.instance.SetIsMoving(true);
    }
}
