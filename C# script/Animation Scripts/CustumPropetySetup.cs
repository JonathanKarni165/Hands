using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class CustumPropetySetup : MonoBehaviourPunCallbacks
{
    private ExitGames.Client.Photon.Hashtable myCustomProperties = new ExitGames.Client.Photon.Hashtable();

    private int ActorNumber;
    private bool isWaitingRoomSceneActive;

    void Start()
    {
        isWaitingRoomSceneActive = SceneManager.GetActiveScene().buildIndex == 1;
        if (isWaitingRoomSceneActive)
            SetCustomProperty();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        SetCustomProperty();
    }

    public void SetCustomProperty()
    {
        for(int i=0; i<PhotonNetwork.PlayerList.Length; i++)
        {
            if(PhotonNetwork.PlayerList[i].IsLocal)
            {
                myCustomProperties["ActorNumber"] = i;
                myCustomProperties["HandColor"] = PlayerPrefs.GetInt("HandColor");
                myCustomProperties["Skin"] = PlayerPrefs.GetInt("Skin");
                myCustomProperties["SkinColor"] = PlayerPrefs.GetInt("SkinColor");
                myCustomProperties["IsMaskGraphic"] = PlayerPrefs.GetInt("IsMaskGraphic");
                PhotonNetwork.SetPlayerCustomProperties(myCustomProperties);
                ActorNumber = i;
            }
        }
    }
}
