using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class player
{
    private int playerNum;
    private TextMeshPro[] tm;
    private int[] fingers;
    private bool[] handsExist;

    public player(int playerNum, TextMeshPro[] tmT)
    {
        int[] fingersT = new int[2];
        bool[] handsExistT = new bool[2];

        for (int i = 0; i < 2; i++)
        {
            fingersT[i] = 1;
            tmT[i].text = "" + 1;
            handsExistT[i] = true;
        }

        this.playerNum = playerNum;
        this.tm = tmT;
        this.fingers = fingersT;
        this.handsExist = handsExistT;
    }
    public int getNumber()
    {
        return this.playerNum;
    }

    public int getFingersNum(int handNum)
    {
        return this.fingers[handNum];
    }

    public void addFingers(int handNum, int add, bool toAdd)
    {
        int sum = this.fingers[handNum] + (toAdd? add: -1 *add);
        int[] fingersT = new int[2];

        for(int i=0; i<2; i++)
        {
            if (i == handNum)
            {
                fingersT[i] = sum % 5;
            }
            else
            {
                fingersT[i] = this.fingers[i];
            }
        }

        
        this.fingers = fingersT;
        this.tm[handNum].text = "" + sum % 5;
    }
}
