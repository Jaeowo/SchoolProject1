using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Singleton
    public static InputManager instance;

    private void Awake()
    {
        // Singleton Setting
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC Å° ´­¸²");
            Application.Quit();
        }
    }

    void CheckInventory()
    {

    }
}
