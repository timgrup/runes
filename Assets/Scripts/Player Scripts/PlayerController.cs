using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    PlayerMovement movement;
    PlayerFocus playerFocus;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        playerFocus = GetComponent<PlayerFocus>();
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

        Interactable focus = playerFocus.GetFocus();
        if (Input.GetMouseButtonDown(1) && focus != null)
        {
            focus.StartInteraction();
        }
    }
}
