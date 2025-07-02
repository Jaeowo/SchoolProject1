using UnityEngine;

public class BoatStat : MonoBehaviour
{
    // Flash Effect
    private SpriteRenderer[] spriteRenderers;
    private float flashDuration = 0.5f;
    private float flashFrequency = 5.0f;
    private bool isFlashing = false;
    private float flashTimer = 0f;

    void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!PlayerInfoManager.instance.GetProgress("EndBoat"))
        //{
        //    FlashEffect();
        //}

        FlashEffect();
    }

    private void FlashEffect()
    {
        if (isFlashing)
        {
            flashTimer -= Time.deltaTime;
            float alpha = Mathf.Abs(Mathf.Sin(Time.time * flashFrequency));

            foreach (var sr in spriteRenderers)
            {
                Color color = sr.color;
                color.a = alpha;
                sr.color = color;
            }

            if (flashTimer <= 0f)
            {
                isFlashing = false;
                foreach (var sr in spriteRenderers)
                {
                    Color color = sr.color;
                    color.a = 1f;
                    sr.color = color;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Cursh!");
            RiverSceneManager.instance.hp -= 1;

            isFlashing = true;
            flashTimer = flashDuration;
        }
    }
}
