using UnityEngine;

public class ChangingPlayerStat : MonoBehaviour
{

    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerInfoManager.instance.GetProgress("FindYuzu"))
        {
            Vector3 savedPos = PlayerInfoManager.instance.LoadPlayerPosition();
            if (savedPos != Vector3.zero)
            {
                transform.position = savedPos;
            }
            ItemManager.instance.GetItem("Yuzu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
