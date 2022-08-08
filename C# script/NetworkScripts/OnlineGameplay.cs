using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class OnlineGameplay : MonoBehaviour
{
    public GameObject passTurn;

    TurnTimer tt;
    PhotonView[] pvs;
    PhotonView myPv;
    OnlinePlayer[] players;
    Button[] buttons;

    private int clickCount, savedFirstHand, savedSecondHand;
    private bool enterence;
    public bool choseToSwitch;
    ClickProp firstSelectedHand, secondSelectedHand;


    void Start()
    {
        tt = FindObjectOfType<TurnTimer>();
        clickCount = 0;
        StartCoroutine(waitUntilSceneLoaded());
        enterence = false;
        choseToSwitch = false;
    }

    public void Init()
    {
        clickCount = 0;
        choseToSwitch = false;
    }

    private void Update()
    {
        if (enterence)
            foreach (Button button in buttons)
            {
                try
                {
                    button.interactable = tt.myTurn;

                    if (!button.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<PhotonView>().IsMine)
                        if (button.gameObject.tag == "DeadHand")
                            button.enabled = false;
                }
                catch
                {
                    print("player left");
                }
            }
    }

    IEnumerator waitUntilSceneLoaded()
    {
        while (PhotonNetwork.PlayerList.Length != FindObjectsOfType<OnlinePlayer>().Length)
            yield return null;

        OnlinePlayer[] firstArray = FindObjectsOfType<OnlinePlayer>();
        buttons = new Button[PhotonNetwork.PlayerList.Length * 2];
        int counter = 0;
        foreach(OnlinePlayer p in firstArray)
        {
            buttons[counter] = p.gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Button>();
            counter++;
            buttons[counter] = p.gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Button>();
            counter++;
        }


        pvs = FindObjectsOfType<PhotonView>();
        for (int i = 0; i < pvs.Length; i++)
        {
            if (pvs[i].IsMine)
                myPv = pvs[i];
        }
        enterence = true;
    }
    
    public void singleClick(OnlinePlayer p, int playerNum, int handNum)
    {
        clickCount += 1;

        if (clickCount == 2)
        {
            secondSelectedHand = new ClickProp(p, playerNum, handNum);
            clickCount = 0;
            if (firstSelectedHand.getPlayerNum() == secondSelectedHand.getPlayerNum() &&
                firstSelectedHand.getHandNum() == secondSelectedHand.getHandNum())
            {
                print("same hand");
                clickCount = 1;
            }
        }
        if (clickCount == 0)
        {
            //switch
            if (firstSelectedHand.getPlayer().getNumber() == secondSelectedHand.getPlayer().getNumber())
            {
                if(!choseToSwitch)
                {
                    print("initSaved");
                    savedFirstHand = firstSelectedHand.getFingers();
                    savedSecondHand = secondSelectedHand.getFingers();
                }

                print(savedSecondHand + " | " + secondSelectedHand.getFingers());

                if (firstSelectedHand.getFingers() % 2 == 0 && secondSelectedHand.getFingers() == 0 && !choseToSwitch)
                {
                    firstSelectedHand.getPlayer().transform.parent.GetComponent<RPC_Functions>()
                        .Resurection(firstSelectedHand, secondSelectedHand);

                    tt.TurnBeggins();
                }

                else if (ValidSwitch())
                {
                    firstSelectedHand.getPlayer().transform.parent.GetComponent<RPC_Functions>()
                        .SwitchFingers(firstSelectedHand, secondSelectedHand);

                    passTurn.SetActive(true);
                    choseToSwitch = true;
                }
                else
                    return;
            }
            //add
            else if(!choseToSwitch)
            {
                print("clicked1");
                secondSelectedHand.getPlayer().transform.parent.GetComponent<RPC_Functions>()
                    .AddFingersAfterClick(secondSelectedHand.getPlayer().getPv(), secondSelectedHand, firstSelectedHand);

                tt.TurnBeggins();
            }
        }
        if (clickCount == 1)
        {
            if (!p.getPv().IsMine)
                clickCount = 0;
            else
                firstSelectedHand = new ClickProp(p, playerNum, handNum);
        }
    }

    public bool ValidSwitch()
    {
        return firstSelectedHand.getFingers() > 0 && secondSelectedHand.getFingers() > 0 &&
                    firstSelectedHand.getFingers() != secondSelectedHand.getFingers() + 1 /*useless turn*/ &&
                    secondSelectedHand.getFingers() + 1 != savedSecondHand  /*back to beggining*/ &&
                    firstSelectedHand.getFingers() - 1 != savedFirstHand &&
                    secondSelectedHand.getFingers() + 1 != savedFirstHand &&
                    firstSelectedHand.getFingers() - 1 != savedSecondHand;
    }
}

