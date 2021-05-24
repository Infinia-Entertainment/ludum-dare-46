using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AttachmentMenu : MonoBehaviour
{
    public GameObject craftMoreButton;
    public GameObject craftButton;
    public GameObject craftButton2;
    public GameObject choicePanel;
    public GameObject moreCraftingPanel;
    public GameObject currentWeapon;

    [SerializeField] private TMP_Text heroCountText;
    [SerializeField] private TMP_Text currentGoldCountText;
    private const string heroCountDescription = "Armed: ";

    public Image[] attachmentImages;

    public Item[] rodAttachments;
    public Item[] attackAttachments;
    public Item[] elementAttachments;

    public TMP_Dropdown[] dropDowns;

    Item selectedRod;
    Item selectedAttack;
    Item selectedElement;



    private void Start()
    {
        UpdateHeroCountUI();
        UpdateCurrentGoldCountUI();
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
        UpdateElements(0);
        attachmentImages[0].sprite = rod.sprite;
        attachmentImages[0].preserveAspect = true;
    }
    void UpdateAttackAttachment(Item attack)
    {
        selectedAttack = attack;
        UpdateElements(1);
        attachmentImages[1].sprite = attack.sprite;
        attachmentImages[1].preserveAspect = true;
    }
    void UpdateElementAttachment(Item element)
    {
        selectedElement = element;
        UpdateElements(2);
        attachmentImages[2].sprite = element.sprite;
        attachmentImages[2].preserveAspect = true;
    }
    public void UpdateHeroCountUI()
    {
        GameStateManager gameManager = GameStateManager.Instance;
        heroCountText.text = heroCountDescription + gameManager.CurrentWeapons.Count.ToString() + "/" + gameManager.CurrentHeroes.Count.ToString();
    }
    public void UpdateCurrentGoldCountUI()
    {
        currentGoldCountText.text = $"Current Gold: {GameStateManager.Instance.CurrentGold}";
    }
    public void Craft()
    {
        if (selectedRod.itemObject != null && selectedAttack.itemObject != null && selectedElement.itemObject != null)
        {
            if (moreCraftingPanel.activeSelf) craftButton2.SetActive(false);
            else craftButton.SetActive(false);
            FindObjectOfType<BlackSmith>().TriggerCrafting();
            Invoke("TriggerCompiling", 2.7f);
        }
    }
    void UpdateElements(int type)
    {
        attachmentImages[type].enabled = true;
        dropDowns[type].gameObject.SetActive(false);

        //Add check for if has enough money and space
        if (selectedRod != null && selectedAttack != null && selectedElement != null && !choicePanel.activeSelf
         && GameStateManager.Instance.IsAffordable(CalculatePrice(selectedRod.price, selectedAttack.price, selectedElement.price))
         )
        {
            if (moreCraftingPanel.activeSelf) craftButton2.SetActive(true);
            else craftButton.SetActive(true);
        }
        else
        {
            if (moreCraftingPanel.activeSelf) craftButton2.SetActive(false);
            else craftButton.SetActive(false);
        }
    }

    void TriggerCompiling()
    {
        currentWeapon = FindObjectOfType<WeaponCreationSystem>().CreateWeapon(selectedRod.itemObject, selectedAttack.itemObject, selectedElement.itemObject);
        GameStateManager.Instance.BuyWeapon(CalculatePrice(selectedRod.price, selectedAttack.price, selectedElement.price), currentWeapon);

        UpdateHeroCountUI();
        UpdateCurrentGoldCountUI();

        if (GameStateManager.Instance.CurrentHeroes.Count <= GameStateManager.Instance.CurrentWeapons.Count)
            craftMoreButton.SetActive(false);
        if (!moreCraftingPanel.activeSelf) choicePanel.SetActive(true);
    }

    private int CalculatePrice(int selectedRodPrice, int selectedAttackPrice, int selectedElementPrice)
    {
        return selectedRodPrice + selectedAttackPrice + selectedElementPrice;
    }


    public void CraftMoreButton()
    {
        choicePanel.SetActive(false);
        moreCraftingPanel.SetActive(true);

        if (selectedRod != null && selectedAttack != null && selectedElement != null && !choicePanel.activeSelf
         && GameStateManager.Instance.IsAffordable(CalculatePrice(selectedRod.price, selectedAttack.price, selectedElement.price))
         )
        {
            craftButton2.SetActive(true);
        }
        else craftButton2.SetActive(false);

        if (currentWeapon != null)
        {
            currentWeapon.GetComponent<Animator>().enabled = false;
        }
        GameStateManager.Instance.UpdateWeaponDisplay();
    }
}
