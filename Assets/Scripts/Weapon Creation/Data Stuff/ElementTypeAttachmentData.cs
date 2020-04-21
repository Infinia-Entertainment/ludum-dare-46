using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class ElementTypeAttachmentData : MonoBehaviour
{
    public ElementAttribute attachmentWeaponElement;
    [Range(0.0f, 2.0f)]
    public float elementModifierStrength;
}
