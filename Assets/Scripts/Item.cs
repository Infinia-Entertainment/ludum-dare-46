using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject {
    public Sprite sprite;
    public GameObject itemObject;
    public string itemName;
    public int price;
    [TextArea(4,10)]
    public string itemDescription;
    public int assignedID;
}
