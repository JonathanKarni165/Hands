using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using TMPro;

public class ButtonAnimation : MonoBehaviour
{
    public TextMeshProUGUI tm;
    public GameObject loadingSym;
    public Animator controller;    
    private bool isPressed = false;

    public void Pressed()
    {
        isPressed = !isPressed;
        controller.SetBool("Pressed", isPressed);
        tm.text = isPressed ? "Populating" : "Quick Match";
        loadingSym.SetActive(isPressed);
    }
}
