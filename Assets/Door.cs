using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    bool open;

    public override void Interact()
    {
        open = !open;

        if(!open)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 90);
        } else
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 0);
        }
    }

}
