using System;
using UnityEngine;

public class BeeMoving : MonoBehaviour
{
    public float dist = 5f;
    public float speed = 4f;
    public float frequency = 5f;
    public float waveHeight = 1.4f;

    Vector3 pos;
    bool dirRight = true;

    void Start()
    {
        pos = transform.position;
    }

    void FixedUpdate()
    {
        if(!HoneySceneManager.instance.isOver)
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


    }

    private void GoRight()
    {
        pos += Vector3.right * Time.deltaTime * speed;
        transform.position = pos + Vector3.up * Mathf.Sin(Time.time * frequency) * waveHeight;

    }

    private void GoLeft()
    {
        pos -= Vector3.right * Time.deltaTime * speed;
        transform.position = pos + Vector3.up * Mathf.Sin(Time.time * frequency) * waveHeight;
    }
}
