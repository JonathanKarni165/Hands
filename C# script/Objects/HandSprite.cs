using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSprite : MonoBehaviour
{
    public Sprite[] HandPositions;
    public Sprite[] Skins;
    public Color[] Colors;

    public Sprite GetHandSprite(int i)
    {
        return HandPositions[i];
    }

    public Sprite GetSkin(int i)
    {
        return Skins[i];
    }

    public Color GetColor(int i)
    {
        return Colors[i];
    }
}
