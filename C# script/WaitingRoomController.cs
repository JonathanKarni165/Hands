using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class WaitingRoomController : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI[] tmArray;
    public GameObject[] kickButtons;
    private PhotonView pview;

    public GameObject kickPanel;
    public int multyPlayerSceneIndex;
    public int menuSceneIndex;
    public int minPlayerToStart;
    public float countdown;

    private int roomSize, playerCount;
    private float countdownTmp;

    public Text timerUI, playerCountUI, roomNumberUI;
    public GameObject startGameButton, PublicButton;

    private bool startCountDown, startGame, isPublic;

    void Start()
    {
        SpawnPv();
        PublicButton.GetComponent<Button>().interactable = false;
        if (PhotonNetwork.IsMasterClient)
        {
            isPublic = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.CurrentRoom.IsOpen = true;
            PublicButton.GetComponent<Button>().interactable = true;
        }

        roomNumberUI.text = PhotonNetwork.CurrentRoom.Name;
        pview = gameObject.GetComponent<PhotonView>();
        countdownTmp = countdown;
        countPlayers();

        if (PhotonNetwork.IsMasterClient)
        {
            startGameButton.SetActive(true);

            foreach (GameObject bt in kickButtons)
                bt.SetActive(true);
        }

        ListPlayers();
    }
    void countPlayers()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        playerCountUI.text = "Players in room\n" + playerCount + ":" + roomSize;

        if (playerCount >= 2)
            startCountDown = true;
        else
        {
            startCountDown = false;
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        countdownTmp = countdown;
        countPlayers();
        ListPlayers();
    }
    [PunRPC]
    private void sendTimer(float timeIn)
    {
        countdownTmp = timeIn;
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startGameButton.SetActive(true);
            PublicButton.GetComponent<Button>().interactable = true;

            foreach (GameObject bt in kickButtons)
                bt.SetActive(true);
        }
        ListPlayers();
        countPlayers();
    }
    private void Update()
    {
        if (startCountDown)
            countdownTmp -= Time.deltaTime;
        else
            countdownTmp = countdown;

        timerUI.text = "Game Countdown\n" + countdownTmp;

        if (countdownTmp <= 0f && !startGame)
        {
            joinMultyPlayerScene();
        }

    }
    public void joinMultyPlayerScene()
    {
        startGame = true;

        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(multyPlayerSceneIndex);
    }
    public void delayCancel()
    {
        SceneManager.LoadScene(menuSceneIndex);
    }

    public void SpawnPv()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "pv"), transform.position, Quaternion.identity);
    }

    public void DestroyPv()
    {
        PhotonView[] PvArray = FindObjectsOfType<PhotonView>();

        foreach (PhotonView pv in PvArray)
        {
            if (pv.Owner.IsLocal)
            {
                Destroy(pv.gameObject);
            }
        }
    }

    public void ListPlayers()
    {
        foreach(TextMeshProUGUI tm in tmArray)
        {
            tm.text = "Empty Slot";
        }

        for(int i=0; i<PhotonNetwork.PlayerList.Length; i++)
        {
            tmArray[i].text = PhotonNetwork.PlayerList[i].NickName;
        }
    }

    public void KickPlayer(int actorNum)
    {
        PhotonView[] PvArray = FindObjectsOfType<PhotonView>();

        foreach(PhotonView pv in PvArray)
        {
            if ((int)pv.Owner.CustomProperties["ActorNumber"] == actorNum)
            {
                pv.gameObject.GetComponent<Pv_RPC>().DisconnectLobby();
                Destroy(pv.gameObject);
            }
        }
    }

    public void OnPublicButton()
    {
        PhotonView[] PvArray = FindObjectsOfType<PhotonView>();

        foreach (PhotonView pv in PvArray)
        {
            if (pv.Owner.IsMasterClient)
            {
                pv.gameObject.GetComponent<Pv_RPC>().PublicSwitch();
            }
        }
        isPublic = !isPublic;
        PhotonNetwork.CurrentRoom.IsVisible = isPublic;
    }

}