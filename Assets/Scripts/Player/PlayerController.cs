using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour, ICharacter
{
    PlayerMovement movement;
    PlayerFocus playerFocus;
    Animator animator;
    public bool alive { get; private set; }

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        playerFocus = GetComponent<PlayerFocus>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Get Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Move
        Vector3 move = transform.right * x + transform.forward * z;
        movement.Move(move);

        if (Input.GetButtonDown("Jump"))
        {
            movement.Jump();
        }

        if (Input.GetMouseButtonDown(0)) //Mouse Left Click
        {
            //ToDo: Add Attack
        }

        Interactable focus = playerFocus.GetFocus();
        if (Input.GetMouseButtonDown(1) && focus != null) //Mouse Right Click if player has focus
        {
            focus.StartInteraction();
        }
    }

    public void Die()
    {
        alive = false;
        enabled = false;
    }
}
