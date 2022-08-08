using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerEnable : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioListener>().enabled = PlayerPrefs.GetInt("Volume") == 1;
    }
}
