using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    int clickedPublicButton = 0, panelClicked = 0;
    public Color[] publicButtonColors;

    public void DisableMe(GameObject me)
    {
        me.SetActive(false);
    }

    public void enablePanel(GameObject gm)
    {
        gm.SetActive(true);
    }

    public void enableOrDisablePanel(GameObject gm)
    {
        panelClicked++;
        gm.SetActive(panelClicked % 2 == 1);
    }

    public void unablePanel(GameObject gm)
    {
        gm.SetActive(false);
    }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void PublicButtonUI(GameObject button)
    {
        clickedPublicButton++;
        button.GetComponent<Image>().color = publicButtonColors[clickedPublicButton % 2];

        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 
            clickedPublicButton % 2 == 0 ? "Private": "Public";
    }
}
