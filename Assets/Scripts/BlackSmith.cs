using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmith : MonoBehaviour
{
    Animator animator;
      
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerCrafting()
    {
        animator.SetTrigger("Ready to Hammer");
    }

    public void SetHammerLoop()
    {
        animator.SetTrigger("Hammer loop");
    }
    public void SetHoldingWeapon()
    {
        animator.SetTrigger("Holding Weapon");
    }
    public void SetHoldingAnim()
    {
        animator.SetTrigger("Holding Anim");
    }
    public void Idle()
    {
        animator.SetTrigger("Idle");
    }

}
