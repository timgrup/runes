using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] float radius = 3.0f;
    public Transform target;
    public Transform interactionTransform;

    bool hasInteracted;
    bool interact;
    bool isFocus;

    private void Update()
    {
        if(interact && !hasInteracted && isFocus && target != null && CanInteract(target))
        {
            Interact();
            hasInteracted = true;
            interact = false;
        }
    }

    //Meant to be overwritten
    public virtual void Interact()
    {
        Debug.Log("Interacting..");
    }

    public void StartInteraction()
    {
        interact = true;
    }

    //Returns true, if target interacting with is in radius
    public bool isReachable(Transform target)
    {
        float distance = Vector3.Distance(target.position, interactionTransform.position);
        if(distance <= radius)
        {
            return true;
        } else
        {
            return false;
        }
    }

    //Returns true, if target may interact with this interactable
    public virtual bool CanInteract(Transform target)
    {
        return isReachable(target);
    }

    // Called when the object starts being focused
    public void OnFocused(Transform target)
    {
        isFocus = true;
        this.target = target;
        hasInteracted = false;
    }

    // Called when the object is no longer focused
    public void OnDefocused()
    {
        isFocus = false;
        target = null;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
