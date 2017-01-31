using UnityEngine;
using System.Collections;

public class Icon : MonoBehaviour
{

    public GameObject buildingPLot;
    public string name;
    private BuildingPlot buildingPlotScript;

    // Use this for initialization
    void Start()
    {
        buildingPlotScript = (BuildingPlot)buildingPLot.GetComponent(typeof(BuildingPlot));
    }

    private void OnMouseDown()
    {
        buildingPlotScript.showBuildMenu(name);
    }
}
