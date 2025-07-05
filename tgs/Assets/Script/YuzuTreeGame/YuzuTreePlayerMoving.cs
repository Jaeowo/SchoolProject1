using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Spine.Unity;

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
    //protected float timeToFloor;

    // For Saving Falling Position
    private Vector3 savingPos;
    private float fallingTimer;

    private bool isGround;

    // Animation
    private SkeletonAnimation skeletonAnim;
    private string currentAnim = "";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        skeletonAnim = GetComponent<SkeletonAnimation>();
        isGround = false;

        SetAnimation("Idle", true);
    }

    void Update()
    {
        if (!isGround)
        {
            FallingCheck();
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

    protected static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    protected IEnumerator NezuParabolaMove()
    {
        savingPos = transform.position;
        isGround = false;
        fallingTimer = 0;
        timer = 0;
        float duration = 1.0f;

        SetAnimation("Jump", false);

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            Vector3 targetPos = Parabola(startPos, endPos, 4, t);
            rb.MovePosition(targetPos);
            yield return null;
        }

    }

    private void FallingCheck()
    {
        float duration = 3.0f;
        fallingTimer += Time.deltaTime;

        if (fallingTimer > duration)
        {
            transform.position = savingPos;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    
        if (collision.gameObject.CompareTag("Branch"))
        {
            isGround = true;
            SetAnimation("Idle", true);
        }
    }

    private void SetAnimation(string name, bool loop)
    {
        if (currentAnim == name)
        {
            return;
        }
        skeletonAnim.AnimationState.SetAnimation(0, name, loop);
        currentAnim = name;
    }
}
