using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonPass : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("SeasonPass"))
            PlayerPrefs.SetInt("SeasonPass", 0);
    }
    
    public void PurchasePass()
    {
        PlayerPrefs.SetInt("SeasonPass", 1);
    }
}
