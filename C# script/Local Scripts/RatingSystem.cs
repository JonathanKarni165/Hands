using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingSystem : MonoBehaviour
{
    public int winMoney, losMoney, winTrophy, losTrophy, tieMoney, tieTrophy;

    private int Trophies;
    private int Money;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Trophies"))
        {
            Trophies = 200;
            PlayerPrefs.SetInt("Trophies", Trophies);
            PlayerPrefs.SetInt("GameCounter", 0);
        }
        if(!PlayerPrefs.HasKey("Money"))
        {
            Money = 0;
            PlayerPrefs.SetInt("Money", Money);
        }

        Trophies = PlayerPrefs.GetInt("Trophies");
        Money = PlayerPrefs.GetInt("Money");
    }

    public void IncreaseGameCounter()
    {
        PlayerPrefs.SetInt("GameCounter", (PlayerPrefs.GetInt("GameCounter")+1));
    }

    public void IncOrDecTrophies(int won)
    {
        int n = PlayerPrefs.GetInt("GameCounter");
        if (won == 1)
        {
            //EASTER EGG 1
            if (Random.Range(1, 10000000) == 98457)
            {
                Trophies = 69;
                Money = 69;
            }

            else
            {
                Trophies += winTrophy;
                Money += winMoney;
            }
        }
        else if(won == 0)
        {
            Trophies -= losTrophy;
            Money += winTrophy;
        }
        else
        {
            Trophies += tieTrophy;
            Money += tieMoney;
        }
        if (Trophies < 0)
            Trophies = 0;

        PlayerPrefs.SetInt("Trophies", Trophies);
        PlayerPrefs.SetInt("Money", Money);
    }
    /*
    private void Update()
    {
        if (Victory.gameEnds && enter)
        {
            print("gameEnds");
            IncOrDecTrophies(Victory.win);
            IncreaseGameCounter();
            enter = false;
        }

        if (Victory.lose && enter)
        {
            print("gameEnds");
            IncOrDecTrophies(false);
            IncreaseGameCounter();
            enter = false;
        }
    }*/

}
