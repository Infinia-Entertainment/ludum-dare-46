using UnityEngine.UI;
using System;
using UnityEngine;

public class Inventory : MonoBehaviour {

     
    public const int numItemSlots = 4;

    [SerializeField]
    GameObject whiteBorder;

    #region Arrays_For_items
    public Image[] itemImages = new Image [numItemSlots];
    public Item[] items = new Item[numItemSlots];
    public GameObject[] itemSlots = new GameObject[numItemSlots];

    #endregion

    //private static bool created = false;
    private int selectionIndex = 0;

    private InteractionController interactionController;

    public static bool isFull = false;

    public Item Hero;

    private void Start()
    {
        interactionController = FindObjectOfType<InteractionController>();
        whiteBorder.SetActive(true);
        AddItem(Hero);
        AddItem(Hero);
        AddItem(Hero);
        AddItem(Hero);
    }

    private void Update()
    {
        GetSelectionIndex();
        Interact();
    }

    public bool Contains(Item itemToCheck, int itemAmount)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].assignedID == itemToCheck.assignedID)
            {
                return true;
            }
        }
        
        return false;
    }

    public void AddItem(Item itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
             if (items[i] == null)
            {
                items[i] = itemToAdd;
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true;
                return;
            }
        }
    }

    public void RemoveItem()
    {
        int i = selectionIndex;
        items[i] = null;
        itemImages[i].sprite = null;
        itemImages[i].enabled = false;
    }

    public void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemToRemove)
            {
                    items[i] = null;
                    itemImages[i].sprite = null;
                    itemImages[i].enabled = false;
                break;
            }
        }
    }



    private void GetSelectionIndex()
    {

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            if (selectionIndex == 3)
            {
                selectionIndex = 0;
            }
            else
            {
                selectionIndex++;
            }

            SelectItemSlot(); 
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if (selectionIndex == 0)
            {
                selectionIndex = 3;
            }
            else
            {
                selectionIndex--;
            }
            
            SelectItemSlot();
        }
    }

    public Item GetCurrentItem()
    {
        if (items[selectionIndex] != null)
            return items[selectionIndex];
        else
            return null;
    }
    
    private void SelectItemSlot()
    {
       
        foreach(GameObject itemSlot in itemSlots)
        {
            int index = Array.IndexOf(itemSlots, itemSlot);
            if (index != selectionIndex) {
                itemSlots[index].GetComponentInChildren<Image>().color = Color.black;
            }
        }
        itemSlots[selectionIndex].GetComponentInChildren<Image>().color = Color.white;
    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (items[selectionIndex] != null)
            {
                int i = selectionIndex;
                interactionController.ApplyInteraction(items[i]);
            }
        }
    }
}
