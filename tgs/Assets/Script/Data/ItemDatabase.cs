using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    [SerializeField] private Sprite[] itemSprite;

    public List<Item> itemList = new List<Item>();

    private void Awake()
    {
        SettingItemData();
    }

    private void SettingItemData()
    {
        //itemList.Add(new Item("001", "Name0", "Desc0", itemSprite[0]));
        //itemList.Add(new Item("002", "Name1", "Desc1", itemSprite[1]));
        //itemList.Add(new Item("003", "Name2", "Desc2", itemSprite[2]));
        //itemList.Add(new Item("004", "Name3", "Desc3", itemSprite[3]));
    }

    public Item GetItemById(string _id)
    {
        return itemList.Find(item => item.Id == _id);
    }
}
