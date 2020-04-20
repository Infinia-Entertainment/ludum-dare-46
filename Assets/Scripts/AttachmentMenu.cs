using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AttachmentMenu : MonoBehaviour
{
    public Image[] attachmentImages;

    public Item[] rodAttachments;
    public Item[] attackAttachments;
    public Item[] elementAttachments;

    public Dropdown[] dropDowns;

    Item selectedRod;
    Item selectedAttack;
    Item selectedElement;

    GameStateManager gameStateManager;

    private void Awake()
    {
        gameStateManager = FindObjectOfType<GameStateManager>();
    }

    public void SlotClicked(int slotType = 1)
    {

        switch (slotType)
        {
            case 1:
                dropDowns[0].gameObject.SetActive(true);

                break;
            case 2:
                dropDowns[1].gameObject.SetActive(true);
                break;
            case 3:
                dropDowns[2].gameObject.SetActive(true);
                break;
            default:
                Debug.LogError("Unkown Case. Contact support for help. Code SET0001");
                break;
        }
    }

    public void RodChoice()
    {
        foreach (Item rod in rodAttachments)
        {
            if (dropDowns[0].value == rod.assignedID)
                UpdateRodAttachment(rod);
        }
    }

    public void AttackChoice()
    {
        foreach (Item attack in attackAttachments)
        {
            if (dropDowns[1].value == attack.assignedID)
                UpdateAttackAttachment(attack);
        }
    }

    public void ElementChoice()
    {
        foreach (Item element in elementAttachments)
        {
            if (dropDowns[2].value == element.assignedID)
                UpdateElementAttachment(element);
        }
    }

    void UpdateRodAttachment(Item rod)
    {
  
        selectedRod = rod;
        attachmentImages[0].sprite = rod.sprite;
        dropDowns[0].gameObject.SetActive(false);
    }
    void UpdateAttackAttachment(Item attack)
    {
        selectedAttack = attack;
        attachmentImages[1].sprite = attack.sprite;
        dropDowns[1].gameObject.SetActive(false);
    }
    void UpdateElementAttachment(Item element)
    {
        selectedElement = element;
        attachmentImages[2].sprite = element.sprite;
        dropDowns[2].gameObject.SetActive(false);
    }

    public void Craft()
    {
        if (selectedRod.itemObject != null && selectedAttack.itemObject != null && selectedElement.itemObject != null)
        {
            GameObject weapon = FindObjectOfType<WeaponCreationSystem>().CreateWeapon(selectedRod.itemObject, selectedAttack.itemObject, selectedElement.itemObject);

            gameStateManager.AddWeapon(weapon);
        }
    }
}
