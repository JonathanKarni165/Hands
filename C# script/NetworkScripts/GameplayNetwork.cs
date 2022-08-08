using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameplayNetwork : MonoBehaviourPunCallbacks
{
    TurnTimer tt;
    PhotonUtil pu;
    public static int playerLeft = 0;
    int count = 0;

    void Start()
    {
        tt = gameObject.GetComponent<TurnTimer>();
        pu = gameObject.GetComponent<PhotonUtil>();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        playerLeft++;
        if(PhotonNetwork.IsMasterClient)
        {
            tt.PlayerLeft((int)otherPlayer.CustomProperties["ActorNumber"]+1);
        }
    }
}
