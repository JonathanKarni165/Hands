using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsButtons : MonoBehaviour
{
    int clicked = 0;
    public Image sprite;
    public Color[] colors;
    public TextMeshProUGUI tm;
    public Button ChangeNameButton, volumeButton;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Volume"))
            PlayerPrefs.SetInt("Volume", 1);

        if (PlayerPrefs.GetInt("Money") >= 100)
            ChangeNameButton.enabled = true;
        else
            ChangeNameButton.enabled = false;

        if (PlayerPrefs.GetInt("Volume") == 0)
        {
            sprite.color = colors[0];
            tm.text = "Volume OFF";
            FindObjectOfType<AudioListener>().enabled = false;
        }
    }

    public void OnClickVolume()
    { 
        bool isVolume = PlayerPrefs.GetInt("Volume") == 0? true: false;
        clicked = isVolume ? 1 : 0;
        FindObjectOfType<AudioListener>().enabled = isVolume;

        sprite.color = colors[clicked];
        PlayerPrefs.SetInt("Volume", clicked);
        tm.text = isVolume ? "Volume ON" : "Volume OFF";
    }

    public void OnClickName()
    {
        FindObjectOfType<RatingUI>().StartCoroutine("MoneyAnim", 100);
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 100);
    }
}
