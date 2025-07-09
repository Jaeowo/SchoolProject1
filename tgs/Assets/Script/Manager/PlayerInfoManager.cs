using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    // Singleton
    public static PlayerInfoManager instance;

    private Dictionary<string, bool> progress = new Dictionary<string, bool>();

    // Player Location
    private Vector3 playerPosition = Vector3.zero;

    // Immovable State
    private bool isMoving = true;

    private void Awake()
    {
        // Singleton Setting
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // After finishing the script, set progress 
    // In addition to conversations, You can save various information

    // Example) PlayerInfoManager.instance.SetProgress("Chapter1.Bird_1", true);
    public void SetProgress(string key, bool value)
    {
        progress[key] = value;
    }

    // Check progress, According to progress, StartConversation...

    // Example)
    // if (PlayerInfoManager.instance.SetProgress("Chapter1.Bird_1"))
    // {
    //      Start dialogue... You want..
    // }

    public bool GetProgress(string key)
    {
        return progress.ContainsKey(key) && progress[key];
    }

    public void SavePlayerPosition(Vector3 pos)
    {
        playerPosition = pos;
    }

    public Vector3 LoadPlayerPosition()
    {
        return playerPosition;
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }

    public void SetIsMoving(bool ismoving)
    {
        isMoving = ismoving;
    }
}
