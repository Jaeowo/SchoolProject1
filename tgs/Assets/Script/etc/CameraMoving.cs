using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public GameObject player;
    private Transform playertransform;

    void Start()
    {
        playertransform = player.transform;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playertransform.position, 2f * Time.deltaTime);
        transform.Translate(0, 0, -10);
    }
}
