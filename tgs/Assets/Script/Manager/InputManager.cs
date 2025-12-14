using UnityEngine;
using UnityEngine.SceneManagement;

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
            Application.Quit();
        }

        // For Debug
        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("EndingScene");
        }
    }
}
