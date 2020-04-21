using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameData;

public class WeaponCreationSystem : MonoBehaviour
{
    //only to use for instantiation
    [SerializeField] private GameObject weaponStructurePrefab;

    //this will connect to UI
    [SerializeField] GameObject selectedRodAttachment;
    [SerializeField] GameObject selectedAttackTypeAttachment;
    [SerializeField] GameObject selectedElementTypeAttachment;

    Animator animator;

    /// <summary>
    /// Returns prefab with the weapon, it contains all info
    /// </summary>
    /// <param name="rodPrefab">rod prefab of the weapon</param>
    /// <param name="attackTypePrefab"> attack type attachement prefab of the weapon</param>
    /// <param name="elementTypePrefab">attack element attachement prefab of the weapon</param>
    /// <returns></returns>
    public GameObject CreateWeapon(GameObject rodPrefab, GameObject attackTypePrefab, GameObject elementTypePrefab)
    {
        GameObject weaponPrefab = Instantiate(weaponStructurePrefab, transform.position, Quaternion.identity);
        StaffWeapon weapon = weaponPrefab.GetComponent<StaffWeapon>();

        Destroy(weapon.rodAttachment);
        Destroy(weapon.attackTypeAttachment);
        Destroy(weapon.elementTypeAttachment);

        weapon.rodAttachment = Instantiate(rodPrefab, weaponPrefab.transform.position, Quaternion.identity,weaponPrefab.transform);
        weapon.attackTypeAttachment =  Instantiate(attackTypePrefab, weaponPrefab.transform.position, Quaternion.identity,weaponPrefab.transform);
        weapon.elementTypeAttachment = Instantiate(elementTypePrefab, weaponPrefab.transform.position, Quaternion.identity, weaponPrefab.transform);

        //RodData stuff = rodPrefab.GetComponent<RodAttachmentData>().weaponType;
        WeaponType weaponType = attackTypePrefab.GetComponent<AttackTypeAttachmentData>().weaponType;
        ElementAttribute weaponElement = elementTypePrefab.GetComponent<ElementTypeAttachmentData>().attachmentWeaponElement;

        RodAttachmentData rodAttachmentData = weapon.rodAttachment.GetComponent<RodAttachmentData>() ;
        ElementTypeAttachmentData elementTypeAttachmentData = weapon.rodAttachment.GetComponent<ElementTypeAttachmentData>();
        AttackTypeAttachmentData attackTypeAttachmentData = weapon.rodAttachment.GetComponent<AttackTypeAttachmentData>();

        weapon.InitializeWeapon(
            weaponType,
            weaponElement,
            attackTypeAttachmentData.attachmentDamage,
            rodAttachmentData.attachmentdefence,
            rodAttachmentData.attachmentFireRate,
            rodAttachmentData.attachmentAttackRange,
            elementTypeAttachmentData.elementModifierStrength);

       
        ConstructWeapon(weapon , rodPrefab, attackTypePrefab, elementTypePrefab); //Construct the visual stuff


        return weaponPrefab; //
    }

    /// <summary>
    /// Changes the prefabs to the appropriate ones and makes stuff work and go brrrrrr
    /// </summary>
    /// <param name="staffWeapon"></param>
    private void ConstructWeapon(StaffWeapon staffWeapon,GameObject rodPrefab, GameObject attackTypePrefab, GameObject elementTypePrefab)
    {

        Transform attackTypeTranform;
        Transform elementTypeTransform;
        //Transform[] buffAttachmentsTransform = new Transform[3];

        Debug.Log(staffWeapon.attackTypeAttachment);

        attackTypeTranform = staffWeapon.attackTypeAttachment.transform;
        elementTypeTransform = staffWeapon.elementTypeAttachment.transform;

        //Assign the positions to transforms
        attackTypeTranform.position = staffWeapon.rodAttachment.GetComponentInChildren<NextSnapPoint>().transform.position;
        elementTypeTransform.position = staffWeapon.attackTypeAttachment.GetComponentInChildren<NextSnapPoint>().transform.position;

    }

    public GameObject CreateTestWeapon()
    {
        return CreateWeapon(selectedRodAttachment, selectedAttackTypeAttachment, selectedElementTypeAttachment);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{   
        //    GameObject weapon = CreateWeapon(selectedRodAttachment, selectedAttackTypeAttachment, selectedElementTypeAttachment);
        //    weapon.transform.position = new Vector3(-3, 0, 0);
        //}
    }



}
