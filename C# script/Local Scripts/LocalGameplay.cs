using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalGameplay : MonoBehaviour
{
    public int playersAmount;
    [SerializeField]
    public DoubleTxt[] tmArray;
    public player[] players;

    private int clickCount;
    ClickProps firstSelectedHand, secondSelectedHand;

    
    void Start()
    {
        players = new player[playersAmount];

        for (int i=0; i<playersAmount; i++)
        {
            player player = new player(i, tmArray[i].tmPro);
            players[i] = player;
        }
        clickCount = 0;
    }

    public void singleClick(player p, int playerNum, int handNum)
    {
        print("hi");
        clickCount += 1;

        if(clickCount == 1)
        {
            firstSelectedHand = new ClickProps(p, playerNum, handNum);
        }

        if (clickCount == 2)
        {
            secondSelectedHand = new ClickProps(p, playerNum, handNum);
            clickCount = 0;
            if (firstSelectedHand.getPlayer() == secondSelectedHand.getPlayer() && 
                firstSelectedHand.getHandNum() == secondSelectedHand.getHandNum())
                clickCount = 1;
        }
        if (clickCount == 0)
        {
            if (firstSelectedHand.getPlayer() == secondSelectedHand.getPlayer())
            {
                if (firstSelectedHand.getPlayer().getFingersNum(firstSelectedHand.getHandNum()) > 1)
                    switchFingersAfterClick();
                else
                    return;
            }
            else
                addFingersAfterClick();
        }
    }

    public void addFingersAfterClick()
    {
        secondSelectedHand.getPlayer().addFingers
           (secondSelectedHand.getHandNum(),
            firstSelectedHand.getPlayer().getFingersNum(firstSelectedHand.getHandNum()), true);
    }

    public void switchFingersAfterClick()
    {
        secondSelectedHand.getPlayer().addFingers(secondSelectedHand.getHandNum(),
            firstSelectedHand.getPlayer().getFingersNum(firstSelectedHand.getHandNum()) -1, true);

        firstSelectedHand.getPlayer().addFingers(firstSelectedHand.getHandNum(),
            firstSelectedHand.getPlayer().getFingersNum(firstSelectedHand.getHandNum()) - 1, false);
    }
}

public class ClickProps
{
    private player p;
    private int playerNum, handNum;

    public ClickProps(player p, int playerNum, int handNum)
    {
        this.p = p;
        this.playerNum = playerNum;
        this.handNum = handNum;
    }

    public int getPlayerNum()
    {
        return this.playerNum;
    }
    public int getHandNum()
    {
        return this.handNum;
    }
    public player getPlayer()
    {
        return this.p;
    }
}
