using UnityEngine;
using System.Collections;
using System;

public class Farm : Building
{
    
    public static int woodCost = -12;
    public int foodPerPuppet;

    // Use this for initialization
    new void Start()
    {
        Debug.Log("atleast i tried");
        base.Start();
        Debug.Log("money cost" + gameManager.buildingMaterials);
    }
    public override bool cost()
    {
        if (gameManager == null)
        {
            Debug.Log("GameManager = null");
        }
        return gameManager.setWood(woodCost);
    }

    protected override void specialBuildingAction()
    {
        //throw new NotImplementedException();
    }
}