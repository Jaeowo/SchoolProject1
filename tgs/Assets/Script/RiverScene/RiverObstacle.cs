using UnityEngine;
using static UnityEditor.PlayerSettings;

public class RiverObstacle : MonoBehaviour
{
    public float speed;
    Vector3 pos;

    private float[] lanes = new float[] { -3f, 0f, 3f };
    private float posX = 12f;
    private float posY = 100f;

    void Start()
    {
        int rand = Random.Range(0, 3);
        posY = lanes[rand];
        pos = new Vector3(posX, posY, 0f);
        transform.position = pos;
    }


    void Update()
    {
        if (!RiverSceneManager.instance.isOver)
        {
            pos -= Vector3.right * Time.deltaTime * speed;
            transform.position = pos;
        }
    }

  
}
