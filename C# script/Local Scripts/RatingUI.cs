using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RatingUI : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI trophies, money;
    private int moneyT;
    void Start()
    {
        trophies.text =
            PlayerPrefs.HasKey("Trophies") ? "" + PlayerPrefs.GetInt("Trophies") : "unrated";

        money.text =
            PlayerPrefs.HasKey("Money") ? "" + PlayerPrefs.GetInt("Money") : "0";
    }
    IEnumerator MoneyAnim(int cost)
    {
        moneyT = PlayerPrefs.GetInt("Money");
        int sum = moneyT - cost;
        for (; moneyT >= sum; moneyT--)
        {
            money.text = "" + moneyT;
            yield return new WaitForSeconds(0.01f);
        }
    }
        



}
