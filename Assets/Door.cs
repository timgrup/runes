using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public Animator _animator;
    bool open;

    public override void Interact()
    {
        open = !open;

        _animator.SetBool("open", open);
    }

}
