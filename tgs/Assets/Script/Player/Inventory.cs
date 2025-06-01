using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Now, Number of slots are fixed
    [SerializeField] private Item[] slots = new Item[4];

    [SerializeField] private ItemDatabase itemDatabase;

    private void Awake()
    {
        // initialize every slots to empty(null)
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = null;
        }
    }

    public void AddItem(string _itemId)
    {

        Item itemToAdd = itemDatabase.GetItemById(_itemId);

        if (itemToAdd == null)
        {
            Debug.Log("Invalid index" + _itemId);
            return;
        }


        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = itemToAdd;
                return;
            }
        }

        Debug.Log("inventory is full");
    }

    public void RemoveItem(int _slotIndex)
    {
        if (slots[_slotIndex] != null)
        {
            slots[_slotIndex] = null;
        }
        else
        {
            Debug.Log("Slot is already empty");
        }
    }
}
