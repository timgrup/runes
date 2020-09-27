using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class UIItemNameDisplay : MonoBehaviour
{
    public static UIItemNameDisplay instance;
    public Camera cam;
    public Text text;
    public bool active;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }
        cam = Camera.main;
        text = GetComponent<Text>();
    }

    void LateUpdate()
    {
        if(active)
        {
            transform.LookAt(transform.position + cam.transform.forward);
        }
    }
    
    public void MoveToItem(ItemPickup itemPickup)
    {
        text.text = itemPickup.item.name;
        transform.position = itemPickup.transform.position + new Vector3(0, 0.75f, 0);
        //ToDo: Find actual height of item
    }

    public void SetActive()
    {
        active = true;
        gameObject.SetActive(true);
    }

    public void SetDeactive()
    {
        active = false;
        gameObject.SetActive(false);
    }
}