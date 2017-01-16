using UnityEngine;
using System.Collections;
using System;

public class Farm : Building
{

    public new int timeToBuild;
    public static int woodCost = -12;
    public new int slotsToBuild;
    public int foodPerPuppet;

    // Use this for initialization
    new void Start()
    {
        Debug.Log("atleast i tried");
        base.Start();
        Debug.Log("money cost" + gameManager.buildingMaterials);

        puppets = new ArrayList(slots);

        buildProgress = 0;
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