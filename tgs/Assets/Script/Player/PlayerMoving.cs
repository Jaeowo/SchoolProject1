using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    // Character rotate by horizontal moving
    private Vector3 rightSide = new Vector3(0, 180, 0);
    private Vector3 leftSide = new Vector3(0, 0, 0);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!ItemManager.instance.GetIsInventoryOpend()
             && !DialogueManager.instance.isInDialogue)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();
            Rotate();
        }
        else
        {
            moveInput = Vector2.zero;
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

}
