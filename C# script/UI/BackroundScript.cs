using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackroundScript : MonoBehaviour
{
    public float speed;
    public bool instance;
    private bool enter = true;
    public RectTransform targetPos, destroyPos;
    
    private void OnEnable()
    {
        enter = true;
    }
    void FixedUpdate()
    {
        transform.Translate(new Vector2(speed, 0));//2.5
        if (!instance)
        {
            if ((speed>0) && transform.position.x >= targetPos.position.x && enter || (speed < 0) && transform.position.x <= targetPos.position.x && enter)
            { 
                FindObjectOfType<BackgroundBehavior>().InstantiateBack();
                enter = false;
            }
            if ((transform.position.x >= destroyPos.position.x && (speed>0))|| ((speed<0) && transform.position.x <= destroyPos.position.x))
                Destroy(gameObject);
        }
    }
}
