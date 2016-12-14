using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BuildingPlot : MonoBehaviour {

    public GameObject DisplayItem;

    public void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            DisplayItem.SetActive(true);        
        }            
    }

    // Use this for initialization
    void Start ()
    {
        Debug.Log("attempt 0");
        DisplayItem.SetActive(false);
    }
}
