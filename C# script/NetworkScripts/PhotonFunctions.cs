using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonFunctions : MonoBehaviour
{
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
