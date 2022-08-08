using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerListCheck : MonoBehaviourPunCallbacks
{ 
    private void Update()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            //if(PhotonNetwork.PlayerList[i].CustomProperties.ContainsKey("ActorNumber"))
                //print(PhotonNetwork.PlayerList[i].CustomProperties["ActorNumber"]);
        }
    }
}
