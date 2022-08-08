using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnlineClick : MonoBehaviour
{
    OnlinePlayer op;
    OnlineGameplay og;
    public void Awake()
    {
        og = FindObjectOfType<OnlineGameplay>();
        op = gameObject.GetComponent<OnlinePlayer>();
    }

    public void OnClick(int hand)
    {
        print("clicked");
        int playerNum = op.getNumber(), handNum = hand;
        og.singleClick(op, playerNum, handNum);      
    }
}
