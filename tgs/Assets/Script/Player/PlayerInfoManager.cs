using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    // Singleton
    public static PlayerInfoManager instance;

    // Player status information
    private bool isStage01Clear = false;

    // Getter & Setter
    public bool IsStae01Clear
    {
        get => isStage01Clear;
        set => isStage01Clear = value;
    }

    private void Awake()
    {
        // Singleton Setting
        if (instance = null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
