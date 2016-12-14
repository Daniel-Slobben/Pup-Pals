using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BuildingPlotMenu : MonoBehaviour {

    public GameObject DisplayItem;

    public void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            DisplayItem.SetActive(false);
        }

    }

    // Use this for initialization
    void Start()
    {
        DisplayItem.SetActive(false);
    }
}
