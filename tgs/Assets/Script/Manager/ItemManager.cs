using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;
using System;
using UnityEngine.UI;

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
    public static ItemManager instance { get; private set; }

    public GameObject inventory;
    public TextAsset itemDatabase;
    public List<Item> allItemList, myItemList;
    public GameObject[] slot;

    public Sprite[] itemSprite;
    public Image[] ItemImage;

    private bool isInventoryOpend = false;

    public bool GetIsInventoryOpend() { return isInventoryOpend; }

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }

        instance = this;

        SettingItemData();
        UpdateItemImages();
        UpdateItemDescription();

        ClearMyItemList();
    }

    private void Update()
    {
        float dpadX = Input.GetAxisRaw("DPad_Horizontal");
        float dpadY = Input.GetAxisRaw("DPad_Vertical");

        //if (dpadX > 0) Debug.Log("DPad Right");
        //else if (dpadX < 0) Debug.Log("DPad Left");

        //if (dpadY > 0) Debug.Log("DPad Up");
        //else if (dpadY < 0) Debug.Log("DPad Down");

        // Test Code
        int currentSelected = myItemList.FindIndex(item => item.isUsing);


        if (Input.GetKeyDown(KeyCode.I) || dpadX < 0)
        {
            inventory.SetActive(true);
            if (myItemList != null)
            {
                isInventoryOpend = true;
                slotSelected(0);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Q)|| dpadX > 0)
        {
            isInventoryOpend = false;
            inventory.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || dpadY > 0)
        {
            int nextIndex = Mathf.Max(0, currentSelected - 1);
            slotSelected(nextIndex);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || dpadY < 0)
        {
            int nextIndex = Mathf.Min(myItemList.Count - 1, currentSelected + 1);
            slotSelected(nextIndex);
        }

        // get & remove test

        //if(Input.GetKeyDown(KeyCode.Z))
        //{
        //    RemoveItem("Pig");
        //}
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    GetItem("Pig");
        //}
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
        string jdata = JsonConvert.SerializeObject(myItemList);
        File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", jdata);

        UpdateItemImages();
        UpdateItemDescription();
    }

    private void Load()
    {
        // string -> deserialize -> list
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.txt");
        myItemList = JsonConvert.DeserializeObject<List<Item>>(jdata);

    }

    public void slotSelected(int slotNum)
    {
        for (int i = 0; i < myItemList.Count; i++)
        {
            myItemList[i].isUsing = (i == slotNum);
        }
        Save();
    }

    private void UpdateItemDescription()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            Transform selected = slot[i].transform.GetChild(0); // "Selected"
            Transform description = selected.GetChild(0);       // "Description"

            if (i < myItemList.Count)
            {
                bool isSelected = myItemList[i].isUsing;
                selected.gameObject.SetActive(isSelected);
                description.gameObject.SetActive(isSelected);

                if (isSelected)
                {
                    TMP_Text itemNameText = description.GetChild(0).GetComponent<TMP_Text>();
                    TMP_Text itemDescText = description.GetChild(1).GetComponent<TMP_Text>();

                    itemNameText.text = myItemList[i].itemName;
                    itemDescText.text = myItemList[i].description;
                }
            }
            else
            {
                selected.gameObject.SetActive(false);
                description.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateItemImages()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (i < myItemList.Count)
            {
                int index = allItemList.FindIndex(x => x.id == myItemList[i].id);
                if (index >= 0 && index < itemSprite.Length)
                {
                    ItemImage[i].sprite = itemSprite[index];
                    ItemImage[i].enabled = true;
                }
                else
                {
                    ItemImage[i].sprite = null;
                    ItemImage[i].enabled = false;
                }
            }
            else
            {
                ItemImage[i].sprite = null;
                ItemImage[i].enabled = false;
            }
        }
    }

    public void GetItem(string id)
    {
        if (myItemList.Count >= slot.Length)
        {
            Debug.Log("full slot");
            return;
        }

        Item addItem = allItemList.Find(item => item.id == id);

        if(addItem != null)
        {
            Item newItem = new Item(addItem.id, addItem.itemName, addItem.description, false);
            myItemList.Add(newItem);

            Save();
        }
        else
        {
            Debug.Log("Item ID doesn't exist");
        }
  
      
    }

    public void RemoveItem(string id)
    {
        Item removeItem = myItemList.Find(item => item.id == id);

        if (removeItem != null)
        {
            myItemList.Remove(removeItem);

            for (int i = 0; i < myItemList.Count; i++)
            {
                myItemList[i].isUsing = false;
            }

            Save();
        }
        else
        {
            Debug.Log("Item doesn't exist So it can't remove item");
        }

    }

    public bool FindItem(string id)
    {
        Item findItem = myItemList.Find(item => item.id == id);
        
        if(findItem != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ClearMyItemList()
    {
        myItemList = new List<Item>(); 
        Save();
    }

}
