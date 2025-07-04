using UnityEngine;

public class Island : MonoBehaviour
{
    private float speed = 2f;
    private bool isArrive = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IslandMoving();
    }

    void IslandMoving()
    {
        if(RiverSceneManager.instance.isClear)
        {
            if(!isArrive)
            {
                transform.position -= Vector3.right * Time.deltaTime * speed;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Arrive");
            isArrive = true;

            // RiverSceneManager -> SceneChange
        }
    }
}
