﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoin : ItemPickup
{
    private Animator anim;
    private CurrencyManager currencyManager;

    new void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        currencyManager = GetComponent<CurrencyManager>();
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
        currencyManager.Add(1);
    }
}
