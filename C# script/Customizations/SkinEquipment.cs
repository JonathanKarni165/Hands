using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkinEquipment : MonoBehaviour
{
    EquipedHands eh;

    public Image[] Hands;
    public GameObject[] Skins;
    public GameObject purchasePanel;
    public Button buyButton;

    private Button itemButton_Instance;
    private CustomizeController itemScript_Instance;
    private string name_instance;
    private int cost_instance;
    private bool isColor_instance;

    public bool mainColor;

    private void Awake()
    {
        eh = gameObject.GetComponent<EquipedHands>();
    }

    public void PurchasePanel(string name, int cost, int trophies, Button itemButton, bool isColor, CustomizeController cc, bool isPass)
    {
        print("panel");
        purchasePanel.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = cost + "ƒ";
        purchasePanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = name;

        purchasePanel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = 
            "REQUIRED: to have more then " + trophies + " trophies";

        if (isPass && PlayerPrefs.GetInt("SeasonPass") == 0)
            purchasePanel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text =
            "Purchase The Season Pass to own this skin";


        if (PlayerPrefs.GetInt("Trophies") >= trophies && PlayerPrefs.GetInt("Money") >= cost &&
            ((isPass && PlayerPrefs.GetInt("SeasonPass") == 1) || !isPass))
        {
            purchasePanel.transform.GetChild(2).transform.GetComponent<Button>().interactable = true;
            itemScript_Instance = cc;
            itemButton_Instance = itemButton;
            name_instance = name;
            cost_instance = cost;

        }
        else
        {
            print(false);
            purchasePanel.transform.GetChild(2).transform.GetComponent<Button>().interactable = false;
        }

        purchasePanel.SetActive(true);
    }

    //oncick
    public void BuyItem()
    {
        PlayerPrefs.SetInt(name_instance, 1);
        itemScript_Instance.UnlockItem();
        itemButton_Instance.gameObject.GetComponent<Image>().color = new Color
             (itemButton_Instance.gameObject.GetComponent<Image>().color.r, itemButton_Instance.gameObject.GetComponent<Image>().color.g,
                itemButton_Instance.gameObject.GetComponent<Image>().color.b, 1);

        FindObjectOfType<RatingUI>().StartCoroutine("MoneyAnim", cost_instance);
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost_instance);
        purchasePanel.SetActive(false);
    }

    public void EquipColor(Color c, int colorCode)
    {
        if (mainColor)
        {
            Hands[0].color = c;
            Hands[1].color = c;

            PlayerPrefs.SetInt("HandColor", colorCode);
        }
        else
        {
            Skins[0].GetComponent<Image>().color = c;
            Skins[1].GetComponent<Image>().color = c;

            PlayerPrefs.SetInt("SkinColor", colorCode);
        }
    }

    public void EquipSkin(Sprite m, int skinCode)
    {
        PlayerPrefs.SetInt("Skin", skinCode);

        if (PlayerPrefs.GetInt("SkinColor") == -1 && PlayerPrefs.GetInt("Skin") != 6)
            FindObjectOfType<Anouncements>().NoticeMessage("Equip secondery color in order to view your equiped skin");

        //mask graphic stickman
        if (PlayerPrefs.GetInt("Skin") == 6)
        {
            PlayerPrefs.SetInt("SkinColor", 11);
            PlayerPrefs.SetInt("IsMaskGraphic", 0);
        }

        else
            PlayerPrefs.SetInt("IsMaskGraphic", 1);

        eh.UpdateHandEquips(false);
    }

    public void ChangeColorOption(TextMeshProUGUI txt)
    {
        mainColor = !mainColor;
        txt.text = mainColor ? "main" : "second"; 
    }

    public void EquipNone()
    {
        PlayerPrefs.SetInt("IsMaskGraphic", 1);
        PlayerPrefs.SetInt("HandColor", -1);
        PlayerPrefs.SetInt("Skin", -1);
        PlayerPrefs.SetInt("SkinColor", -1);

        eh.UpdateHandEquips(false);
    }
}
