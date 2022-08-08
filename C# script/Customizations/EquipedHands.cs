using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EquipedHands : MonoBehaviour
{
    public Image[] myHandSprite;
    public Image[] myHandSkin;
    HandSprite hs;
    
    void Start()
    {
        hs = gameObject.GetComponent<HandSprite>();
        UpdateHandEquips(true);
    }

    public void UpdateHandEquips(bool changePos)
    {
        //Stickman skin color
        if (PlayerPrefs.GetInt("Black") == 0 && PlayerPrefs.GetInt("Skin") != 6 && PlayerPrefs.GetInt("SkinColor") == 11)
            PlayerPrefs.SetInt("SkinColor", -1);

        int HandColor = PlayerPrefs.GetInt("HandColor");
        int Skin = PlayerPrefs.GetInt("Skin");
        int SkinColor = PlayerPrefs.GetInt("SkinColor");
        int ShowMaskGraphic = PlayerPrefs.GetInt("IsMaskGraphic");

        for (int i = 0; i < 2; i++)
        {
            if(changePos)
                myHandSprite[i].sprite = hs.GetHandSprite(Random.Range(0, 5));

            if (ShowMaskGraphic == 0)
                myHandSprite[i].gameObject.GetComponent<Mask>().showMaskGraphic = false;
            else
                myHandSprite[i].gameObject.GetComponent<Mask>().showMaskGraphic = true;

            if (HandColor != -1)
            {
                try
                {
                    myHandSprite[i].color = hs.GetColor(HandColor);
                }
                catch
                {
                    print(HandColor);
                }
            }
            else
                myHandSprite[i].color = Color.white;

            if (Skin != -1)
            {
                myHandSkin[i].sprite = hs.GetSkin(Skin);

                if (SkinColor != -1)
                {
                    try
                    {
                        myHandSkin[i].color = hs.GetColor(SkinColor);
                    }
                    catch
                    {
                        print(SkinColor);
                    }
                }
                else
                    myHandSkin[i].color = new Color(0, 0, 0, 0);
            }
            else
            {
                //myHandSkin[i].sprite = null;
                myHandSkin[i].color = new Color(0, 0, 0, 0);
            }

            
        }
    }

    public void ChangeHandPos()
    {
        for (int i = 0; i < 2; i++)
        {
            myHandSprite[i].sprite = hs.GetHandSprite(Random.Range(0, 5));
        }
    }
}
