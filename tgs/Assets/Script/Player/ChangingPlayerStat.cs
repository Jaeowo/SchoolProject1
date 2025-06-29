using System.Collections;
using UnityEngine;

public class ChangingPlayerStat : MonoBehaviour
{
    private bool finishYuzu = false;
    private bool finishHoney = false;

    void Start()
    {
        if(!finishYuzu)
        {
            if (PlayerInfoManager.instance.GetProgress("FindYuzu"))
            {
                Vector3 savedPos = PlayerInfoManager.instance.LoadPlayerPosition();
                if (savedPos != Vector3.zero)
                {
                    transform.position = savedPos;
                }
                ItemManager.instance.GetItem("Yuzu");
                finishYuzu = true;
            }

        }
 
        if (!finishHoney)
        {
            if (PlayerInfoManager.instance.GetProgress("CollectHoney"))
            {
                Vector3 savedPos = PlayerInfoManager.instance.LoadPlayerPosition();
                if (savedPos != Vector3.zero)
                {
                    transform.position = savedPos;
                }
                ItemManager.instance.GetItem("Honey");
                finishHoney = true;

            }
        }

    }

}
