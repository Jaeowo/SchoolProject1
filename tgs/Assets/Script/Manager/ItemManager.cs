using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string id;
    private string itemName;
    private string description;
    private bool isUsing;
    //private Sprite itemImage;

    // Constructor
    public Item(string _id, string _itemName, string _description, bool _isUsing)
    {
        id = _id;
        itemName = _itemName;
        description = _description;
        isUsing = _isUsing;
        //itemImage = _itemImage;
    }

    // Getter&Setter
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    //public Sprite ItemImage { get; private set; }
}

public class ItemManager : MonoBehaviour
{
    [SerializeField] private Sprite[] itemSprite;

    public List<Item> allItemList, myItemList;
    public TextAsset itemDatabase;


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
    }
}