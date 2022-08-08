using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RPCheck : MonoBehaviour
{
    public PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
            pv.RPC("RPC_movePlayer", PhotonNetwork.PlayerList[1]);
    }

    [PunRPC]
    void RPC_movePlayer()
    {
        gameObject.transform.position = new Vector3(0, 0, 0);
    }
}
