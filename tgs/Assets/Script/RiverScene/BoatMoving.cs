using UnityEngine;

public class BoatMoving : MonoBehaviour
{
    private float[] lanes = new float[] { 2.9f, 5.8f, 8.7f };
    private int currentLane = 1;

    // Floating Effect
    private float frequency = 1.5f;
    private float floatRange = 0.5f;
    private Vector3 basePosition;

    private void Start()
    {
        basePosition = transform.position;
        basePosition.y = lanes[currentLane];
    }

    void Update()
    {
        //if (!PlayerInfoManager.instance.GetProgress("EndBoat"))
        //{
        //    HandleLaneInput();
        //    ApplyFloating();
        //}

        if(!RiverSceneManager.instance.isOver && !RiverSceneManager.instance.isClear)
        {
            HandleLaneInput();
            ApplyFloating();
        }
    }

    void HandleLaneInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            if (currentLane > 0)
            {
                currentLane--;
                basePosition.y = lanes[currentLane];
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            if (currentLane < lanes.Length - 1)
            {
                currentLane++;
                basePosition.y = lanes[currentLane];
            }
        }
    }

    void ApplyFloating()
    {
        float yOffset = Mathf.PerlinNoise(Time.time * 0.5f, 0f) * floatRange;
        float zRot = Mathf.Sin(Time.time * (frequency * 0.8f)) * 2f;

        transform.position = basePosition + Vector3.up * yOffset;
        transform.rotation = Quaternion.Euler(0, 0, zRot);
    }


}
