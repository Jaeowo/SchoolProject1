using UnityEngine;
using static UnityEditor.PlayerSettings;

public class RiverObstacle : MonoBehaviour
{
    private float speed;

    // Position
    Vector3 pos;
    private float[] lanes = new float[] { -3f, 0f, 3f };
    private float posX = 12f;
    private float posY = 100f;

    // Type
    private SpriteRenderer spriteRenderer; 
    public Sprite[] sprites;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Random Lane
        int laneRand = Random.Range(0, 3);
        posY = lanes[laneRand];
        pos = new Vector3(posX, posY, 0f);
        transform.position = pos;

        // Random Type
        int typeRand = Random.Range(0, 3);
        switch (typeRand)
        {
            case 0:
                TypeNum00Set();
                break;
            case 1:
                TypeNum01Set();
                break;
            case 2:
                TypeNum02Set();
                break;
            default:
                break;
        }
    }

    private void TypeNum00Set()
    {
        // Shark
        if (sprites[0] != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = sprites[0];
            speed = 14f;
        }
        else
        {
            Debug.Log("There isn't a sprite");
        }
    }

    private void TypeNum01Set()
    {
        // Stone
        if (sprites[1] != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = sprites[1];
            speed = 9f;
        }
        else
        {
            Debug.Log("There isn't a sprite");
        }
    }
    private void TypeNum02Set()
    {
        // Branch
        if (sprites[2] != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = sprites[2];
            speed = 7f;
        }
        else
        {
            Debug.Log("There isn't a sprite");
        }
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
