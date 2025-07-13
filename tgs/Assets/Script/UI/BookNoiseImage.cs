using UnityEngine;
using UnityEngine.UI;

public class BookNoiseImage : MonoBehaviour
{
    public Image[] targetImages;
    public Texture2D noiseTexture;
    public float dissolveSpeed = 1.0f;

    private Material[] materials;
    private float dissolveAmount = 0f;
    private bool isDissolving = false;

    void Start()
    {
        materials = new Material[targetImages.Length];

        for (int i = 0; i < targetImages.Length; i++)
        {
            // 원본 머티리얼 복사해서 각각 설정
            materials[i] = Instantiate(targetImages[i].material);
            targetImages[i].material = materials[i];

            // 이미지 텍스처 자동 설정
            if (targetImages[i].sprite != null)
                materials[i].SetTexture("_MainTex", targetImages[i].sprite.texture);

            // 노이즈 텍스처와 초기 dissolve 값 설정
            materials[i].SetTexture("_NoiseTex", noiseTexture);
            materials[i].SetFloat("_Dissolve", 0f);
            materials[i].SetFloat("_DissolveSmoothness", 0.03f);
        }
    }

    void Update()
    {
        if (isDissolving)
        {
            dissolveAmount += Time.deltaTime * dissolveSpeed;
            dissolveAmount = Mathf.Clamp01(dissolveAmount);

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].SetFloat("_Dissolve", dissolveAmount);
            }

            if (dissolveAmount >= 1f)
            {
                foreach (var img in targetImages)
                    img.enabled = false;

                isDissolving = false;
            }
        }
    }

    public void StartDissolve()
    {
        dissolveAmount = 0f;
        isDissolving = true;
    }
}
