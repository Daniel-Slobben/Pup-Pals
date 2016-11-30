using UnityEngine;
using System.Collections;
using System;

public class BuildingPlot : MonoBehaviour {

    public GameObject DisplayItem;

    public void OnMouseDown()
    {
        DisplayItem.SetActive(true);
    }

    // Use this for initialization
    void Start ()
    {
        DisplayItem.SetActive(false);
    }
}
