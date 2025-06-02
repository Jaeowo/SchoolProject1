using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class Item
{
    public string id;
    public string itemName;
    public string description;
    public bool isUsing;

    // Constructor
    public Item(string _id, string _itemName, string _description, bool _isUsing)
    {
        id = _id;
        itemName = _itemName;
        description = _description;
        isUsing = _isUsing;
    }
}

public class ItemManager : MonoBehaviour
{

    public TextAsset itemDatabase;
    public List<Item> allItemList, myItemList;


    private void Awake()
    {
        SettingItemData();
    }

    private void SettingItemData()
    {
        // Load AllItemList
        string[] line = itemDatabase.text.Substring(0, itemDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            allItemList.Add(new Item(row[0], row[1], row[2], row[3] == "TRUE"));
        }

        Load();

    }

    private void Save()
    {
        // list -> serialize(Json) -> string
        string jdata = JsonConvert.SerializeObject(allItemList);
        File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", jdata);
    }

    private void Load()
    {
        // string -> deserialize -> list
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.txt");
        myItemList = JsonConvert.DeserializeObject<List<Item>>(jdata);
    }
}