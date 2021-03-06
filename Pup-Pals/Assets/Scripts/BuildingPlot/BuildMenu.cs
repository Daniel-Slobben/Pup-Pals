﻿using UnityEngine;
using System.Collections;

public class BuildMenu : MonoBehaviour {
    public GameObject buildingPLot;
    public GameObject text;
    public string info;
    private BuildingPlot buildingPlotScript;

    // Use this for initialization
    void Start () {
        buildingPlotScript = (BuildingPlot)buildingPLot.GetComponent(typeof(BuildingPlot));
        Invoke("setInfo", 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setInfo()
    {
        if (info == "NotEnoughMoney")
        {
            text.GetComponent<TextMesh>().text = "Not enough money";
        }
        else
        {
            string textToShow = "";
            if (info == "Farm")
            {
                textToShow = info + System.Environment.NewLine + "Cost: " + Mathf.Abs(Farm.woodCost);
            }
            if (info == "Workshop")
            {
                textToShow = info + System.Environment.NewLine + "Cost: " + Mathf.Abs(Workshop.woodCost);
            }
            if (info == "School")
            {
                textToShow = info + System.Environment.NewLine + "Cost: " + Mathf.Abs(School.woodCost);
            }
            if (info == "Sanitation Building")
            {
                textToShow = "Sanitation" + System.Environment.NewLine + "Cost: " + Mathf.Abs(SanitationBuilding.woodCost);
            }
            text.GetComponent<TextMesh>().text = textToShow;
        }

    }
    private void OnMouseDown()
    {
        //buildingPlotScript.build(info);
        Debug.Log("builidng now: "+info);
        buildingPlotScript.build(info);
    }
}
