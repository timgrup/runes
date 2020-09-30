using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    protected PlayerFocus playerFocus;

    public Item item;
    public float uiTextOffset = 1.0f;

    protected void Start()
    {
        playerFocus = PlayerManager.instance.player.GetComponent<PlayerFocus>();
    }

    public override void Interact()
    {
        Debug.Log("Picking up: " + item.name);
        playerFocus.RemoveFocus();
        Destroy(gameObject);
    }
}
