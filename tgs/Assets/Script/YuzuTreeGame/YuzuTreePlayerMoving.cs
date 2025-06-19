using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class YuzuTreePlayerMoving : MonoBehaviour
{
    private Vector2 moveInput;

    private Rigidbody2D rb;

    // Character rotate by horizontal moving
    private Vector3 rightSide = new Vector3(0, 180, 0);
    private Vector3 leftSide = new Vector3(0, 0, 0);

    // For parabola moving
    private Vector3 startPos, endPos;
    protected float timer;
    protected float timeToFloor;

    private bool isGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGround = false;
    }

    void Update()
    {
        if (!isGround)
        {
            return;
        }

        if (!PlayerInfoManager.instance.GetProgress("FindYuzu"))
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton2))
            {

                transform.rotation = Quaternion.Euler(leftSide);
                startPos = transform.position;
                endPos = startPos + new Vector3(-7, 0, 0);
                StartCoroutine("NezuParabolaMove");
            }

            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                transform.rotation = Quaternion.Euler(rightSide);
                startPos = transform.position;
                endPos = startPos + new Vector3(7, 0, 0);
                StartCoroutine("NezuParabolaMove");
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                startPos = transform.position;
                endPos = startPos + new Vector3(0, 2, 0);
                StartCoroutine("NezuParabolaMove");
            }
        }
    }

    private void FixedUpdate()
    {
        //Vector2 targetPos = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
        //rb.MovePosition(targetPos);
    }


    protected static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    protected IEnumerator NezuParabolaMove()
    {
        isGround = false;
        timer = 0;
        float duration = 1.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            Vector3 targetPos = Parabola(startPos, endPos, 4, t);
            rb.MovePosition(targetPos);
            yield return null;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    
        if (collision.gameObject.CompareTag("Branch"))
        {
            isGround = true;
        }
    }

}
