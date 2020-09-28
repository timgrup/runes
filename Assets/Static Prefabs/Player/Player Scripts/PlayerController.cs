using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    PlayerMovement movement;
    Camera cam;

    public LayerMask interactionMask;
    public Interactable focus;
    public float interactRadius = 1.0f;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        cam = Camera.main;
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

        //ToDo: Raycast vielleicht ungenau Winkelbezogen
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.SphereCast(ray, interactRadius, out hit, Mathf.Infinity, interactionMask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                if (interactable.CanInteract(transform))
                {
                    Debug.Log("Interaction available with: " + interactable.name);
                    SetFocus(interactable);
                }
                else
                {
                    RemoveFocus();
                }
            }
        }
        else
        {
            RemoveFocus();
        }

        if (Input.GetMouseButtonDown(1) && focus != null)
        {
            focus.StartInteraction();
        }
    }

    public void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }

            focus = newFocus;

            if (newFocus.GetType() == typeof(ItemPickup))
            {
                UIItemNameDisplay.instance.SetActive();
                UIItemNameDisplay.instance.MoveToItem((ItemPickup)newFocus);
            }
        }
        newFocus.OnFocused(transform);
    }

    public void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
            focus = null;
            UIItemNameDisplay.instance.SetDeactive();
        }
    }
}
