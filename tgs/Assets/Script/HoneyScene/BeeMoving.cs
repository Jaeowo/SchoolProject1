using System;
using UnityEngine;

public class BeeMoving : MonoBehaviour
{
    private float dist = 5f;
    private float speed = 4f;
    private float frequency = 5f;
    private float waveHeight = 1.4f;

    Vector3 pos;
    bool dirRight = true;

    void Start()
    {
        pos = transform.position;
    }

    void FixedUpdate()
    {
        if (transform.position.x > dist)
        {
            dirRight = false;
        }
        else if (transform.position.x < -dist)
        {
            dirRight = true;
        }

        if (dirRight)
        {
            GoRight();
        }
        else
        {
            GoLeft();
        }
    }

    private void GoRight()
    {
        pos += transform.right * Time.deltaTime * speed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * waveHeight;

    }

    private void GoLeft()
    {
        pos -= transform.right * Time.deltaTime * speed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * waveHeight;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Cursh!");
        HoneySceneManager.instance.HP -= 1;
    }
}
