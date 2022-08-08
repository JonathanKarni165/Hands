using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Anouncements : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI tm;

    public void NoticeMessage(string message)
    {
        tm.text = message;
        panel.SetActive(true);
    }

    public void OkButton()
    {
        panel.SetActive(false);
    }
}
