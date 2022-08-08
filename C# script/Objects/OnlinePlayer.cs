using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class OnlinePlayer : MonoBehaviour
{
    private int HandColor, Skin, SkinColor;
    public TextMeshProUGUI nameText;
    public Image[] myHandSprite; 
    public Image[] myHandSkin; 

    PhotonView pv;
    HandSprite hs;
    RPC_Functions rpcF;

    private int playerNum;
    private int[] fingers;
    private bool[] handsExist;
    private bool isMaskGraphic;

    IEnumerator WaitUntilSceneSetup()
    {
        while (!RotateScene.finished)
            yield return null;

        NameInit();
    }

    void Start()
    {
        rpcF = gameObject.transform.parent.GetComponent<RPC_Functions>();
        this.pv = gameObject.transform.parent.GetComponent<PhotonView>();
        this.playerNum = (int)this.pv.Owner.CustomProperties["ActorNumber"];
        hs = gameObject.GetComponent<HandSprite>();

        this.HandColor = (int)this.pv.Owner.CustomProperties["HandColor"];
        this.Skin = (int)this.pv.Owner.CustomProperties["Skin"];
        this.SkinColor = (int)this.pv.Owner.CustomProperties["SkinColor"];
        this.isMaskGraphic = (int)this.pv.Owner.CustomProperties["IsMaskGraphic"] == 1;

        

        int[] fingersT = new int[2];
        bool[] handsExistT = new bool[2];
        for (int i = 0; i < 2; i++)
        {
            fingersT[i] = 1;
            myHandSprite[i].sprite = hs.GetHandSprite(1);
            myHandSprite[i].color = hs.GetColor(playerNum);

            if(!isMaskGraphic)
                myHandSprite[i].gameObject.GetComponent<Mask>().showMaskGraphic = false;

            if (HandColor != -1)
                myHandSprite[i].color = hs.GetColor(HandColor);

            if (Skin != -1)
                myHandSkin[i].sprite = hs.GetSkin(Skin);

            if (SkinColor != -1)
                myHandSkin[i].color = hs.GetColor(SkinColor);

            handsExistT[i] = true;
        }


        this.fingers = fingersT;
        this.handsExist = handsExistT;
        
        StartCoroutine(WaitUntilSceneSetup());
    }

    public void NameInit()
    {
        nameText.text = this.pv.Owner.NickName;
        nameText.color = hs.GetColor(playerNum);
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
        int sum = this.fingers[handNum] + (toAdd ? add : -1 * add);

        int[] fingersT = new int[2];
        Sprite[] handSpriteT = new Sprite[2];

        for (int i = 0; i < 2; i++)
        {
            if (i == handNum)
            {
                fingersT[i] = sum % 5;
                handSpriteT[i] = hs.GetHandSprite(sum % 5);
            }
            else
            {
                fingersT[i] = this.fingers[i];
                handSpriteT[i] = this.myHandSprite[i].sprite;
            }
        }


        this.fingers = fingersT;
        this.myHandSprite[0].sprite = handSpriteT[0];
        this.myHandSprite[1].sprite = handSpriteT[1];

        FindObjectOfType<AudioManager>().Play("knak");
    }

    public PhotonView getPv()
    {
        return this.pv;
    }

    public bool[] GetHandsExist()
    {
        return this.handsExist;
    }

    public void SetHandExist(int hand, bool exist)
    {
        this.handsExist[hand] = exist;
    }

    public bool GetState()
    {
        return this.handsExist[0] || this.handsExist[1];
    }

    public void EliminatePlayer()
    {
        this.fingers[0] = 0;
        this.fingers[1] = 0;

        this.handsExist[0] = false;
        this.handsExist[1] = false;

        this.myHandSprite[0].sprite = hs.GetHandSprite(0);
        this.myHandSprite[1].sprite = hs.GetHandSprite(0);
    }
}
