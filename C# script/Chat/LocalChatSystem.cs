using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using RTLTMPro;

public class LocalChatSystem : MonoBehaviour
{
    PhotonUtil pu;
    public GameObject notificationSign;
    public TMP_InputField inputTextSet;
    public TextMeshProUGUI inputTextGet, chatText, myChatText;
    private void Start()
    {
        pu = FindObjectOfType<PhotonUtil>();
        notificationSign.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            OnSentClicked();
    }

    public void OnSentClicked()
    {
        if(inputTextGet.text != "")
            pu.GetMyRpc().SendMesssage(inputTextGet.text);
        print("text");
        inputTextSet.text = "";
    }

    public void PostMessage(string nickname, string message, bool isLocal)
    { 
        message = FlipString(message);

        if(isLocal)
        {
            myChatText.text += "\n" + message + " :YOU"; 
            for(int i=0; i< EscapeCounter(message) +1; i++)
            {
                chatText.text += "\n----";
            }
        }
        else
        {
            notificationSign.SetActive(true);
            chatText.text += "\n" + nickname + ": " + message;
            for (int i = 0; i < EscapeCounter(message) + 1; i++)
            {
                myChatText.text += "\n----";
            }
        }
    }


    public static bool IsEnglish(string txt)
    {
        for (int i = 0; i < txt.Length - 1; i++)
        {
            if (((int)(txt.ToCharArray()[i]))< 32 || ((int)(txt.ToCharArray()[i])) > 125)
                return false;
        }
        return true;
    }

    public static string FlipString(string txt)
    {
        string output = "";
        for(int i =txt.Length-2; i>=0; i--)
        {
            output += txt.ToCharArray()[i];
        }
        return output;
    }

    public static int EscapeCounter(string str)
    {
        return str.ToCharArray().Length / 20 +1;
    }
}
