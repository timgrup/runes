using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocus : MonoBehaviour
{
    Camera cam;
    [SerializeField] float interactRadius = 1.0f;
    [SerializeField] LayerMask interactionMask;
    private Interactable focus;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction, Color.green);
        RaycastHit hit;
        bool sphereCast = Physics.SphereCast(ray, interactRadius, out hit, Mathf.Infinity, interactionMask);
        bool rayCast = false;

        if (!sphereCast)
            rayCast = Physics.Raycast(ray, out hit, Mathf.Infinity, interactionMask);

        if (sphereCast || rayCast)
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

            //ToDo: if Abfrage verbessern
            if (newFocus.GetType() == typeof(ItemPickup) || newFocus.GetType().IsSubclassOf(typeof(ItemPickup)))
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

    public Interactable GetFocus()
    {
        return this.focus;
    }
}
