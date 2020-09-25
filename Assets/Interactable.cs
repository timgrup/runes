using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    float radius = 3.0f;
    Transform target;

    //Meant to be overwritten
    public virtual void Interact()
    {
        Debug.Log("Interacting with" + target.name);
    }

    //Returns true, if target interacting with is in radius
    public bool isReachable()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= radius)
        {
            return true;
        } else
        {
            return false;
        }
    }

    //Returns true, if target may interact with this interactable
    public virtual bool CanInteract()
    {
        return isReachable();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
