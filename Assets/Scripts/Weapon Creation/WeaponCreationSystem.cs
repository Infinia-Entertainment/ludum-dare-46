using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static WeaponData;

public class WeaponCreationSystem : MonoBehaviour
{
    //only to use for instantiation
    [SerializeField] private GameObject weaponStructurePrefab;

    //this will connect to UI
    private WeaponType weaponType;
    private WeaponElement weaponElement;


    public GameObject CreateWeapon(WeaponType weaponType, WeaponElement weaponElement, params WeaponBuffAttachement[] weaponBuffAttachement)
    {
        GameObject weaponPrefab = Instantiate(weaponStructurePrefab, transform.position, Quaternion.identity);

        StaffWeapon weapon = weaponPrefab.GetComponent<StaffWeapon>();

        weapon.InitializeWeapon(weaponType, weaponElement, weaponBuffAttachement);

        ConstructWeapon(weapon); //Construct the visual stuff

        return weaponPrefab; //
    }

    //i need to create a prefab just that lol

    /// <summary>
    /// Changes the prefabs to the appropriate ones and makes stuff work and go brrrrrr
    /// </summary>
    /// <param name="staffWeapon"></param>
    private void ConstructWeapon(StaffWeapon staffWeapon)
    {

        //Get all transforms
        //Transform rodTransform = GetComponent<Transform>();
        Transform attackTypeTranform = GetComponent<Transform>();
        Transform elementTypeTransform = GetComponent<Transform>();

        Transform[] buffAttachmentsTransform = new Transform[3];

        foreach (GameObject buffAttachment in staffWeapon.buffAttachments)
        {
            staffWeapon.buffAttachments.Add(buffAttachment);
        }

        //Get data which contains positions for where to put the next attachment
        AttachmentData rodAttachmentData = staffWeapon.rodAttachment.GetComponent<AttachmentData>();
        AttachmentData attackTypeAttachmentData = staffWeapon.attackTypeAttachment.GetComponent<AttachmentData>();

        //Assign the positions to transforms
        attackTypeTranform.position = rodAttachmentData.nextAttachmentbuildPos;
        elementTypeTransform.position = attackTypeAttachmentData.nextAttachmentbuildPos;

        // cache buff positions
        Vector3[] buffPositions = staffWeapon.rodAttachment.GetComponent<AttachmentData>().buffAttachmentPositions;

        //Assign extra buff positions
        for (int i = 0; i < buffPositions.Length; i++)
        {
            buffAttachmentsTransform[0].position = buffPositions[0];
        }
    }

}
