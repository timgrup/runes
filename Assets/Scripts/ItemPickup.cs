using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    PlayerController pc;

    public Item item;
    public float uiTextOffset = 1.0f;

    void Start()
    {
        pc = PlayerManager.instance.player.GetComponent<PlayerController>();
    }

    public override void Interact()
    {
        Debug.Log("Picking up: " + item.name);
        pc.RemoveFocus();
        Destroy(gameObject);
    }
}
