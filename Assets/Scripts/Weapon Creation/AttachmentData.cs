using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentData : MonoBehaviour
{
    public Vector3 attachmentPivotPos;
    public Vector3 nextAttachmentbuildPos;

    [Tooltip("Only the rod needs to have these positions filled in lol")]
    public Vector3[] buffAttachmentPositions = new Vector3[3];
}
