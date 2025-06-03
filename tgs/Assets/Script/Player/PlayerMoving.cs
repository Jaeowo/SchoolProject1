using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    private Vector2 moveInput;

    // Character rotate by horizontal moving
    private Vector3 rightSide = new Vector3(0, 180, 0);
    private Vector3 leftSide = new Vector3(0, 0, 0);

    void Start()
    {
    }

    void Update()
    {
        if (!ItemManager.Instance.GetIsInventoryOpend())
        {
            Move();
            Rotate();
        }
        
    }

    private void Move()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(moveInput.x, moveInput.y, 0).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;

     


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
