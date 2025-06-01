using UnityEngine;

public class Item
{
    private string id;
    private string itemName;
    private string description;
    private Sprite itemImage;

    // Constructor
    public Item(string _id, string _itemName, string _description, Sprite _itemImage)
    {
        id = _id;
        itemName = _itemName;
        description = _description;
        itemImage = _itemImage;
    }

    // Getter&Setter
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Sprite ItemImage { get; private set; }
}
