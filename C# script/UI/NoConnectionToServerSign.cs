using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class NoConnectionToServerSign : MonoBehaviour
{
    Image sign;
    bool enter1, enter2;

    IEnumerator WaitForInitialConnection()
    {
        yield return new WaitForSeconds(1);
        enter1 = true;
        enter2 = true;
    }

    IEnumerator CheckLongConnection()
    {
        print("kickconect");
        enter2 = false;
        if(!PhotonNetwork.IsConnectedAndReady)
        {
            yield return new WaitForSeconds(3);
            if(!PhotonNetwork.IsConnectedAndReady && SceneManager.GetActiveScene().buildIndex != 0)
            {
                PlayerPrefs.SetInt("ConnectKick", 1);
                SceneManager.LoadScene(0);
            }
        }
        enter2 = true;
    }

    void Start()
    {
        sign = gameObject.GetComponent<Image>();
        StartCoroutine("WaitForInitialConnection");
    }

    void Update()
    {
        sign.enabled = !PhotonNetwork.IsConnected && enter1;
        if (!PhotonNetwork.IsConnected && enter2)
            StartCoroutine("CheckLongConnection");
    }
}
