using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Victory : MonoBehaviour
{
    public PhotonUtil pu;
    public EndgameUI egu;
    public RatingSystem rs;
    private bool enter1, enter2, enter3;
    public bool tie, isGameEnded;

    void Start()
    { 
        enter1 = true;
        enter2 = false;
        enter3 = true;
        tie = false;
        isGameEnded = false;
    }

    void Update()
    {
        if (pu.start && enter1)
        {
            enter2 = true;
            enter1 = false;
        }

        if (enter2)
        {
            CheckWin();
            if (!pu.myPlayer.transform.GetChild(0).GetComponent<OnlinePlayer>().GetState())
            {
                enter2 = false;
                Lose();
            }
        }
    }

    public void CheckWin()
    {
        foreach (GameObject player in pu.players)
        {
            try
            {
                if (!player.GetComponent<PhotonView>().IsMine &&
                    player.transform.GetChild(0).GetComponent<OnlinePlayer>().GetState())
                    return;
            }

            catch
            {
                return;
            }
        }
        Win();
    }


    public void Win()
    {
        egu.SetupEndGamePanel(1, 0);
        pu.GetMyRpc().LoseGame();
        rs.IncOrDecTrophies(1);
        isGameEnded = true;
    }

    public void Lose()
    {
        rs.IncOrDecTrophies(0);
        isGameEnded = true;
    }

    public void Draw()
    {
        enter2 = false;
        tie = true;
        rs.IncOrDecTrophies(3);
        egu.SetupEndGamePanel(2, 0);
        pu.GetMyRpc().LoseGame();
        isGameEnded = true;
    }
}
