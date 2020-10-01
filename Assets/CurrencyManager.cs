using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public int amount {get; private set;}

    private void Awake()
    {
        instance = this;
    }

    public void Add(int value)
    {
        if (value > 0)
        {
            amount += value;
        }
        Debug.Log("Currency: " + this.amount);
    }
}
