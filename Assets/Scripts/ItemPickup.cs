using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public PlayerController pc;

    public float uiTextOffset = 1.0f;

    void Awake()
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
