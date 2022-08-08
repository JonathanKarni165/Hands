using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class EndgameUI : MonoBehaviour
{
    public GameObject panel;

    Victory vc;
    RatingSystem rs;

    public Color[] panelColors;
    public Image panelBack;

    public GameObject rematchButton;
    public GameObject rematchText;

    public TextMeshProUGUI title;
    public TextMeshProUGUI trophyUI;
    public TextMeshProUGUI moneyUI;

    private string[] playersColors = { "RED", "BLUE", "GREEN", "YELLOW" };
    private bool endMusicEnter;

    private void Awake()
    {
        vc = FindObjectOfType<Victory>();
        rs = FindObjectOfType<RatingSystem>();
        endMusicEnter = true;
    }

    public void SetupEndGamePanel(int win, int winnerNum)
    {
        panel.SetActive(true);
        if (endMusicEnter)
        {
            FindObjectOfType<AudioManager>().Play(win == 1 ? "victory" : "lose");
            endMusicEnter = false;
        }
        title.text = win ==  1? "YOU WON!" : win == 0? playersColors[winnerNum] + " WON": "TIE"; 
        panelBack.color = win == 1? panelColors[0] : win == 0? panelColors[1]: panelColors[2];
        trophyUI.text = win == 1? "+" + rs.winTrophy : win == 0? "-" + rs.losTrophy: "+" + rs.tieTrophy;
        moneyUI.text = win == 1? "+"+ rs.winMoney : win == 0? "+" + rs.losMoney: "+" + rs.losMoney;
    }

    private void Update()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            rematchButton.SetActive(true);
            rematchText.SetActive(false);
        }
    }
}
