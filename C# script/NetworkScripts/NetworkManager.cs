using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        ConnectionRetry();
        if(PlayerPrefs.GetInt("ConnectKick") == 1)
        {
            FindObjectOfType<Anouncements>().NoticeMessage("kicked to main menu: no connection to server");
            PlayerPrefs.SetInt("ConnectKick", 0);
        }
    }

    private void Update()
    {
        print("connection" + PhotonNetwork.IsConnected);
        if (!PhotonNetwork.IsConnected)
            ConnectionRetry();
    }
    public void ConnectionRetry()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Successfuly connected to server from region " + PhotonNetwork.CloudRegion);
        PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("username");
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    public void LeaveLobby()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
}
