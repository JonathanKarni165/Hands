using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickProp 
{
    private OnlinePlayer p;
    private int playerNum, handNum;

    public ClickProp(OnlinePlayer p, int playerNum, int handNum)
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
    public OnlinePlayer getPlayer()
    {
        return this.p;
    }
    public int getFingers()
    {
        return this.p.getFingersNum(this.handNum);
    }
}
