using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeController : MonoBehaviour
{
    public string itemName;
    public int cost;
    public int trophiesRequaired, colorCode, skinCode;
    public bool isColor;
    public bool isPass;
    public Image skin;
    Color color;
    public Button mybutton;
    SkinEquipment se;

    private void Start()
    {
        color = gameObject.GetComponent<Image>().color;
        se = FindObjectOfType<SkinEquipment>();
        if(!PlayerPrefs.HasKey(itemName))
        {
            PlayerPrefs.SetInt(itemName, 0);
        }

        UnlockItem();
    }

    public void UnlockItem()
    {
        print("unlock item");
        if (isColor)
        {
            if (PlayerPrefs.GetInt(itemName) == 0)
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            else
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            if (PlayerPrefs.GetInt(itemName) == 0)
                gameObject.GetComponent<Image>().color = FindObjectOfType<SkinButtonColors>().skinButtonColors[0];
            else
                gameObject.GetComponent<Image>().color = FindObjectOfType<SkinButtonColors>().skinButtonColors[1];
        }
    }

    public void Pressed()
    {
        if (PlayerPrefs.GetInt(itemName) == 0)
        {
            se.PurchasePanel(itemName, cost, trophiesRequaired, mybutton, isColor, this, isPass);
        }

        else if(PlayerPrefs.GetInt(itemName) == 1)
        {
            if (isColor)
            {
                se.EquipColor(color, colorCode);
            }
            else
            {
                se.EquipSkin(skin.sprite, skinCode);
            }
        }
    }
}
