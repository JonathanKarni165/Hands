using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class TurnTimer : MonoBehaviour
{
    OnlineGameplay og;
    public int timerT, timerValue, colorNum;
    private int temp, roomSize;
    private bool enterence, enterence1, enterence2;
    public bool myTurn;
    public PhotonUtil pu;
    public TextMeshProUGUI timerTM;
    public Color[] colors;
    private bool[] missingPlayers;
    private int actorNum;

    private void Awake()
    {
        actorNum = (int)PhotonNetwork.LocalPlayer.CustomProperties["ActorNumber"];
        og = FindObjectOfType<OnlineGameplay>();

        myTurn = false;
        timerValue = 10;
        colorNum = 1;

        enterence = true;
        enterence1 = true;
        enterence2 = true;

        missingPlayers = new bool[4];
        for(int i=0; i<missingPlayers.Length; i++)
        {
            missingPlayers[i] = false;
        }

        roomSize = PhotonNetwork.PlayerList.Length;
        temp = 0;

        if (PhotonNetwork.PlayerList.Length == 3)
        {
            colorNum++;
            temp = 1;
        }
        if (PhotonNetwork.PlayerList.Length == 4)
        {
            colorNum += 2;
            temp = 2;
        }

        
    }

    private void Update()
    {
        timerTM.text = " " + timerValue;
        timerTM.color = colors[colorNum % roomSize];

        myTurn = (colorNum % roomSize) == ((actorNum+1+1+temp) % roomSize);

        if (timerValue <= 0 && enterence && myTurn)
        {
            if (!og.choseToSwitch)
                pu.myPlayer.GetComponent<RPC_Functions>().Disqualification();

            else
                OnPassClick();

            StartCoroutine("EnterenceCooldown");
            enterence = false;
        }

        if (pu.start)
        {
            //is dead
            if (!pu.GetMyOp().GetState() && myTurn && enterence1)
            {
                TurnBeggins();
                enterence1 = false;
                StartCoroutine("Enterence1Cooldown");
            }

            //player left
            if (missingPlayers[colorNum % roomSize] && enterence2)
            {
                TurnBeggins();
                enterence2 = false;
                StartCoroutine("Enterence2Cooldown");
            }
        }
    }

    IEnumerator EnterenceCooldown()
    {
        yield return new WaitForSeconds(2f);
        enterence = true;
    }
    IEnumerator Enterence1Cooldown()
    {
        yield return new WaitForSeconds(2f);
        enterence1 = true;
    }
    IEnumerator Enterence2Cooldown()
    {
        yield return new WaitForSeconds(2f);
        enterence2 = true;
    }

    public void FirstTurn()
    {
        timerValue = timerT;
        timerTM.color = Color.red;
        
        if(PhotonNetwork.IsMasterClient)
            pu.masterPlayer.GetComponent<RPC_Functions>().StartNewTimer();
            
    }

    public void TurnBeggins()
    {
        og.choseToSwitch = false;
        if (PhotonNetwork.IsMasterClient)
            pu.masterPlayer.GetComponent<RPC_Functions>().StartNewTimer();
        else
        {
            print("!master");
            pu.masterPlayer.GetComponent<RPC_Functions>().SendTimerToMaster();
        }
    }

    public void OnPassClick()
    {
        TurnBeggins();
        GameObject.FindGameObjectWithTag("pass").SetActive(false);
    }

    public void PlayerLeft(int playerNum)
    {
        missingPlayers[playerNum - 1] = true;
    }
}
