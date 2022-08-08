using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.IO;

public class GameSceneSetup : MonoBehaviour
{
    public RectTransform[] boardPlaces;
    private Transform[] placesArray;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            placesArray = new Transform[4];
            for (int i = 0; i < 4; i++)
            {
                print(i);
                placesArray[i] = boardPlaces[i].transform;
            }
        }
    }

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            return;
        }
        CreatePlayer();
    }
    public void CreatePlayer()
    {
        print("creating player");
        Transform spawnProperties = placesArray[(int)PhotonNetwork.LocalPlayer.CustomProperties["ActorNumber"]];
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), spawnProperties.position, spawnProperties.rotation);
    }
    
    public Transform[] GetPositions()
    {
        return placesArray;
    }
}

