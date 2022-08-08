using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehavior : MonoBehaviour
{
    public GameObject BackGround;
    public RectTransform SpawnPoint;

    public void InstantiateBack()
    {
        Instantiate(BackGround, SpawnPoint.position, SpawnPoint.rotation, transform).
            GetComponent<BackroundScript>().instance = false;
    }
}
