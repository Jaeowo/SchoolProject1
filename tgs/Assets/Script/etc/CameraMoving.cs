using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = player.transform;
    }

    void Update()
    {
        if (DialogueManager.instance != null && DialogueManager.instance.isInDialogue)
            return;

        Vector3 targetPos = new Vector3(playerTransform.position.x, playerTransform.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, targetPos, 2f * Time.deltaTime);
    }
}
