using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AttachmentMenu : MonoBehaviour
{
    public Image[] attachmentImages;

    public Item[] rodAttachments;
    public Item[] attackAttachments;
    public Item[] elementAttachments;

    public TMP_Dropdown[] dropDowns;

    Item selectedRod;
    Item selectedAttack;
    Item selectedElement;

    public GameObject craftButton;
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
        UpdateElements(0);
        attachmentImages[0].sprite = rod.sprite;
    }
    void UpdateAttackAttachment(Item attack)
    {
        selectedAttack = attack;
        UpdateElements(1);
        attachmentImages[1].sprite = attack.sprite;
    }
    void UpdateElementAttachment(Item element)
    {
        selectedElement = element;
        UpdateElements(2);
        attachmentImages[2].sprite = element.sprite;
    }

    public void Craft()
    {
        if (selectedRod.itemObject != null && selectedAttack.itemObject != null && selectedElement.itemObject != null)
        {

            
            FindObjectOfType<BlackSmith>().TriggerCrafting();
            Invoke("TriggerCompiling", 2.7f);

        }
    }
    void UpdateElements(int type)
    {
        attachmentImages[type].enabled = true;
        dropDowns[type].gameObject.SetActive(false);
        if(selectedRod != null && selectedAttack != null && selectedElement != null)
        {
            craftButton.SetActive(true);
        }
    }

    void TriggerCompiling()
    {
        GameObject weapon = FindObjectOfType<WeaponCreationSystem>().CreateWeapon(selectedRod.itemObject, selectedAttack.itemObject, selectedElement.itemObject);
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.AddWeapon(weapon);
        }
    }
}
