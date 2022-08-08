using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetUsername : MonoBehaviour
{
    public GameObject usernamePanel;
    public TextMeshProUGUI tm;
    private void Awake()
    {
        if(!PlayerPrefs.HasKey("username"))
        {
            usernamePanel.SetActive(true);
        }
    }

    public void UsernameSubmition()
    {
        if (tm.text.Length > 2)
        {
            PlayerPrefs.SetString("username", tm.text);
            usernamePanel.SetActive(false);
        }
    }
    
}
