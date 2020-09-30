using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoin : ItemPickup
{
    Animator anim;

    new void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    public override void Interact()
    {
        playerFocus.RemoveFocus();
        GetComponent<SphereCollider>().enabled = false;
        anim.SetTrigger("flipCoin");
        StartCoroutine(CallBase()) ;
    }

    IEnumerator CallBase()
    {
        yield return new WaitForSeconds(anim.runtimeAnimatorController.animationClips[0].length);
        base.Interact();
    }
}
