using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RPC_Functions : MonoBehaviour
{
    PhotonView pv;
    TurnTimer tt;
    DestroyPlayers dp;
    OnlinePlayer op;
    Victory vic;
    OnlineGameplay og;
    LocalChatSystem lcs;
    EndgameUI egu;
    PhotonUtil pu;
    public static int turnCounter;
    private int suddenDeathTurns;

    private void Start()
    {
        pv = gameObject.GetComponent<PhotonView>();
        tt = FindObjectOfType<TurnTimer>();
        dp = FindObjectOfType<DestroyPlayers>();
        vic = FindObjectOfType<Victory>();
        op = gameObject.transform.GetChild(0).GetComponent<OnlinePlayer>();
        og = FindObjectOfType<OnlineGameplay>();
        lcs = FindObjectOfType<LocalChatSystem>();
        egu = FindObjectOfType<EndgameUI>();
        pu = FindObjectOfType<PhotonUtil>();

        turnCounter = 0;
        suddenDeathTurns = 0;
    }

    public void AddFingersAfterClick(PhotonView myPv, ClickProp firstSelectedHand, ClickProp secondSelectedHand)
    {
        myPv.RPC("RPC_AddFingersAfterClick", RpcTarget.All, 
            firstSelectedHand.getHandNum(), secondSelectedHand.getFingers());
    }

    public void SwitchFingers(ClickProp firstSelectedHand, ClickProp secondSelectedHand)
    {
        pv.RPC("RPC_SwitchFingers", RpcTarget.All, 1, firstSelectedHand.getHandNum(), 
            secondSelectedHand.getHandNum());
    }

    public void Resurection(ClickProp firstSelectedHand, ClickProp secondSelectedHand)
    {
        pv.RPC("RPC_Resurection", RpcTarget.All, firstSelectedHand.getFingers()/2 
            , firstSelectedHand.getHandNum(), secondSelectedHand.getHandNum());
    }

    public void SendTimerToMaster()
    {
        pv.RPC("RPC_SendTimerToMaster", RpcTarget.MasterClient);
    }
    public void StartNewTimer()
    {
        if (!vic.isGameEnded)
        {
            turnCounter++;

            if (pu.AlivePlayersCount() == 2)
                suddenDeathTurns++;

            pv.RPC("RPC_ChangeColour", RpcTarget.All, suddenDeathTurns);
            StartCoroutine("DecreaseTimer", turnCounter);
        }
    }
    IEnumerator DecreaseTimer(int currentTurn)
    {
        for(int i=0; i< tt.timerT && currentTurn == turnCounter && !vic.isGameEnded; i++)
        {
            yield return new WaitForSeconds(1f);

            if(currentTurn == turnCounter)
                pv.RPC("RPC_DecreaseTimer", RpcTarget.All);
        }
    }

    public void DestroyHand(int handNum)
    {
        pv.RPC("RPC_DestroyHand", RpcTarget.All, handNum);
    }

    public void LoseGame()
    {
        pv.RPC("RPC_LoseGame", RpcTarget.Others, (int)PhotonNetwork.LocalPlayer.CustomProperties["ActorNumber"]);
    }

    public void Disqualification()
    {
        pv.RPC("RPC_Disqualification", RpcTarget.All);
    }

    public void SendMesssage(string message)
    {
        lcs.PostMessage(PhotonNetwork.NickName, message, true);
        pv.RPC("RPC_SendMessage", RpcTarget.Others, message, PhotonNetwork.NickName);
    }
    /*
    public void SendPrefs()
    {
        if (pv.Owner.IsLocal)
        {
            pv.RPC("RPC_SendPrefs", RpcTarget.All, PlayerPrefs.GetInt("HandColor"),
                PlayerPrefs.GetInt("Skin"), PlayerPrefs.GetInt("SkinColor"));
        }
    }*/
    

    //RPC functions

    [PunRPC]
    public void RPC_AddFingersAfterClick(int handNum, int fingerNum)
    {
        gameObject.transform.GetChild(0).GetComponent<OnlinePlayer>().
                addFingers(handNum, fingerNum,true);
    }

    [PunRPC]
    public void RPC_SwitchFingers(int toAdd, int firstHand, int secondHand)
    {
        gameObject.transform.GetChild(0).GetComponent<OnlinePlayer>().
            addFingers(secondHand, toAdd, true);

        gameObject.transform.GetChild(0).GetComponent<OnlinePlayer>().
            addFingers(firstHand, toAdd, false);
    }

    [PunRPC]
    public void RPC_Resurection(int toAdd, int firstHand, int secondHand)
    {
        gameObject.transform.GetChild(0).GetComponent<OnlinePlayer>().
            addFingers(secondHand, toAdd, true);

        gameObject.transform.GetChild(0).GetComponent<OnlinePlayer>().
            addFingers(firstHand, toAdd, false);

        gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(secondHand)
            .GetComponent<Image>().color = Color.white;

        op.SetHandExist(secondHand, true);
        gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).
            transform.GetChild(secondHand).tag = "AliveHand";
    }

    [PunRPC]
    public void RPC_ChangeColour(int newTimerMode)
    {
        tt.timerValue = tt.timerT;

        if(newTimerMode > 4)
        {
            tt.timerValue = 10;

            if (newTimerMode > 6)
                tt.timerValue = 5;
            if (newTimerMode > 8)
                tt.timerValue = 3;

            if (newTimerMode > 10)
                vic.Draw();
        }

        tt.colorNum++;
    }

    [PunRPC]
    public void RPC_DecreaseTimer()
    {
        if(tt.timerValue <= 10)
            FindObjectOfType<AudioManager>().Play("clock");

        tt.timerValue--;
    }

    [PunRPC]
    public void RPC_SendTimerToMaster()
    {
        StartNewTimer();
        og.Init();
    }

    [PunRPC]
    public void RPC_DestroyHand(int handNum)
    {
        gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0)
            .transform.GetChild(handNum).GetComponent<Image>().color = Color.gray;

        gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0)
            .transform.GetChild(handNum).tag = "DeadHand";

        op.SetHandExist(handNum, false);
    }

    [PunRPC]
    public void RPC_LoseGame(int winingPlayerNum)
    {
        if(!vic.tie)
            egu.SetupEndGamePanel(0, winingPlayerNum);
    }

    [PunRPC]
    public void RPC_Disqualification()
    {
        op.EliminatePlayer();
    }

    [PunRPC]
    public void RPC_SendMessage(string message, string nickname)
    {
        lcs.PostMessage(nickname, message, false);
    }
}
