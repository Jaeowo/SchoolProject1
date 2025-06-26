using UnityEngine;

public class HoneyComb : MonoBehaviour
{
    private bool isColliding = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (isColliding)
        {
            if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                HoneySceneManager.instance.gettingHoney += 1;
                Destroy(gameObject);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = true;
        }
    }
}
