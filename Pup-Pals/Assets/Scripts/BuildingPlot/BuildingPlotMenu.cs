using UnityEngine;
using System.Collections;

public class BuildingPlotMenu : MonoBehaviour {

    public GameObject DisplayItem;

    public void OnMouseDown()
    {
        DisplayItem.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        DisplayItem.SetActive(false);
    }
}
