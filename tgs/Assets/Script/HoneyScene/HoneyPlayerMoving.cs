using UnityEngine;

public class HoneyPlayerMoving : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    // Character rotate by horizontal moving
    private Vector3 rightSide = new Vector3(0, 180, 0);
    private Vector3 leftSide = new Vector3(0, 0, 0);

    // Flash Effect
    private SpriteRenderer spriteRenderer;
    private float flashDuration = 0.5f;
    private float flashFrequency = 5.0f;
    private bool isFlashing = false;
    private float flashTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!PlayerInfoManager.instance.GetProgress("CollectHoney"))
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();
            Rotate();
            FlashEffect();
        }
        else
        {
            moveInput = Vector2.zero;
        }

    }

    private void FlashEffect()
    {
        if(isFlashing)
        {
            flashTimer -= Time.deltaTime;
            float alpha = Mathf.Abs(Mathf.Sin(Time.time * flashFrequency));
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;

            if(flashTimer <=0f)
            {
                isFlashing = false;
                color.a = 1f;
                spriteRenderer.color = color;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetPos = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPos);
    }

    private void Rotate()
    {
        if (moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(rightSide);
        }
        else if (moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(leftSide);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Cursh!");
            HoneySceneManager.instance.hp -= 1;

            isFlashing = true;
            flashTimer = flashDuration;
        }
    }
 
}
