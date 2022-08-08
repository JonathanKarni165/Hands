using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMenu : MonoBehaviour
{
    public GameObject Panel;
    public RectTransform[] places;
    public float speed;
    private Vector3 position;

    private void Start()
    {
        position = places[1].position;
    }

    public void SwitchPanel(int PanelNumber)
    {
        position = places[PanelNumber].position;
    }

    private void Update()
    {
        if (Panel.GetComponent<RectTransform>().position != position)
        {

            Panel.GetComponent<RectTransform>().position = Vector3.MoveTowards
            (Panel.transform.position, position, speed * Time.deltaTime);
        }
    }
}
