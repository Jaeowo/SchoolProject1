using UnityEngine;

public class CameraManagerHoney : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = player.transform;
    }

    void Update()
    {
        Vector3 playerPos = playerTransform.position;
        Vector3 newPlayerCameraPos = new Vector3(playerPos.x, playerPos.y , -10f);
        transform.position = Vector3.Lerp(transform.position, newPlayerCameraPos, 2f * Time.deltaTime);

        //transform.position = newPlayerCameraPos;
    }
}
