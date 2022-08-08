using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewAdjustment : MonoBehaviour
{
    private RectTransform rt;
    public float[] InitialPos, RightPos, LeftPos;
    private bool onScroll;

    private void Awake()
    {
        rt = gameObject.GetComponent<RectTransform>();
        onScroll = false;
    }

    private void Update()
    {
        //print(rt.offsetMax.x);
    }
    public void OnScroll()
    {
        print("scroll");
    }

    IEnumerator WaitForScroll()
    {
        yield return new WaitForSeconds(0.5f); 
        if (rt.offsetMax.x > (-1*InitialPos[1]))
        {
            rt.SetLeft(RightPos[0]);
            rt.SetRight(RightPos[1]);
        }
        else
        {
            rt.SetLeft(LeftPos[0]);
            rt.SetRight(LeftPos[1]);
        }
    }
}
