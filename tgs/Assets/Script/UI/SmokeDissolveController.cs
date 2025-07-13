using UnityEngine;

public class SmokeDissolveController : MonoBehaviour
{
    public Material dissolveMaterial;    // 위 머티리얼 직접 할당
    public Texture2D noiseTexture;       // 노이즈 텍스처 직접 할당

    [Range(0f, 5f)]
    public float dissolveRange = 0f;

    public float dissolveSpeed = 1f;

    private Material matInstance;

    void Start()
    {
        // 머티리얼 인스턴스 생성 (원본 머티리얼 건드리지 않도록)
        matInstance = Instantiate(dissolveMaterial);
        GetComponent<SpriteRenderer>().material = matInstance;

        matInstance.SetTexture("_NoiseTex", noiseTexture);
        matInstance.SetVector("_StPt", new Vector4(0.5f, 0.5f, 0, 0)); // 중앙에서 시작

        matInstance.SetFloat("_DissolveRange", 0);
        matInstance.SetFloat("_DissolveExp", 1);
        matInstance.SetFloat("_DistortionStrength", 0.3f);
        matInstance.SetFloat("_BlurStrength", 2.0f);
    }

    void Update()
    {
        if (dissolveRange < 1f)
        {
            dissolveRange += Time.deltaTime * dissolveSpeed;
            dissolveRange = Mathf.Min(dissolveRange, 1f);
            matInstance.SetFloat("_DissolveRange", dissolveRange);
        }
    }
}
