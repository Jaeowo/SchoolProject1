using System.Collections;
using UnityEngine;

public class ChangingPlayerStat : MonoBehaviour
{

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
