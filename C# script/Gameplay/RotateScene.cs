using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class RotateScene : MonoBehaviour
{ 
    PhotonView pv;
    GameSceneSetup scSetup;
    Transform[] places;
    public Color[] colors;
    public Image[] playerBoard;
    public RectTransform targetPosRect;
    private Transform targetPos;
    GameObject[] players;
    GameObject localPlayer;
    public static bool finished = false;

    void Start()
    {
        targetPos = targetPosRect.transform;
        pv = gameObject.GetComponent<PhotonView>();
        scSetup = FindObjectOfType<GameSceneSetup>();
        places = scSetup.GetPositions();


        StartCoroutine(waitUntilSceneLoaded());
        

    }

    IEnumerator waitUntilSceneLoaded()
    {
        while (PhotonNetwork.PlayerList.Length != FindObjectsOfType<OnlinePlayer>().Length)
            yield return null;

        players = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().IsMine)
                localPlayer = player;
        }
        rotationIteration();

        yield return new WaitForSeconds(0.2f);

        ChangeBoardColor();

        finished = true;
    }

    public void rotationIteration()
    {
        while(localPlayer.transform.position != targetPos.transform.position)
        {
            RotatePlayers();
        }
    }

    public void RotatePlayers()
    {
        foreach (GameObject player in players)
        {
            bool oneItrt = true;
            for (int i =0; i< places.Length; i++)
            {
                if(player.transform.position == places[i].transform.position && oneItrt)
                {
                    player.transform.position = places[(i + 1) % places.Length].transform.position;
                    player.transform.rotation = places[(i + 1) % places.Length].transform.rotation;
                    oneItrt = false;
                }
            }
        }
    }

    public void ChangeBoardColor()
    {
        for(int i=0; i< playerBoard.Length; i++)
        {
            playerBoard[i].color = colors[((int)PhotonNetwork.LocalPlayer.CustomProperties["ActorNumber"] + i) % 4];
        }
    }

    
}
