using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnHandClick : MonoBehaviour
{
    LocalGameplay lg;
    public void Awake()
    {
        lg = FindObjectOfType<LocalGameplay>();
    }

    public void OnClick(int clickProp)
    {
        int playerNum = clickProp / 10, handNum = clickProp % 10;
        player[] players = lg.players;

        foreach (player p in players)
            if (p.getNumber() == playerNum)
            {
                print("hello");
                lg.singleClick(p, playerNum, handNum);
            }
    }
}
