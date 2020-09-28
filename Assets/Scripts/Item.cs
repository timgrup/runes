using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public string description = "";
    public Sprite icon = null;
}
