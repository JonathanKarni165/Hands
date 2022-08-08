using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class StartGameButton : MonoBehaviourPunCallbacks
{
    public override void OnPlayerLeftRoom(Player p)
    {
        if (PhotonNetwork.IsMasterClient)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
    void start()
    {
        print("hi");
        if (PhotonNetwork.IsMasterClient)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
