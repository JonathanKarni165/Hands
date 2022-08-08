using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class SpawnAvatar : MonoBehaviour
{
    PhotonView pv;
    public Color[] characterColors;
    public GameObject AvatarPrefab;
    
    void Start()
    {
        pv = gameObject.GetComponent<PhotonView>();

        if(pv.IsMine)
            pv.RPC("SpawningAvatar", RpcTarget.AllBuffered, (int)PhotonNetwork.LocalPlayer.CustomProperties["ActorNumber"]);
    }

    [PunRPC]
    void SpawningAvatar(int playerNum)
    {
        GameObject LocalAvatar = AvatarPrefab;
        
        //foreach(SpriteRenderer spr in LocalAvatar.GetComponentsInChildren<SpriteRenderer>())
        //{
        //    spr.color = characterColors[playerNum];
        //}

        foreach(Button btn in LocalAvatar.GetComponentsInChildren<Button>())
        {

            ColorBlock cb = btn.colors;
            cb.pressedColor = characterColors[playerNum];
            btn.colors = cb;
        }
        
        Instantiate(LocalAvatar, transform.position, transform.rotation, transform);
    }
}
