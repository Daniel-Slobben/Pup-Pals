using UnityEngine;
using System.Collections;
using System;

public class Farm : Building
{

    public int timeToBuild;
    public static int woodCost = -12;
    public int slotsToBuild;
    public int foodPerPuppet;

    public string state; // 4 states: None, Sowing, Growing, Harvest

    private int buildProgress; // if this reaches timeToBuild the building is done
    private int lastTurn;
    private bool build;


    // Use this for initialization
    new void Start()
    {
        Debug.Log("atleast i tried");
        base.Start();
        Debug.Log("money cost" + gameManager.buildingMaterials);

        puppets = new ArrayList(slots);

        GetComponent<SpriteRenderer>().sprite = construction;

        buildProgress = 0;
        lastTurn = gameManager.turnNumber;
    }
    public override bool cost()
    {
        if (gameManager == null)
        {
            Debug.Log("GameManager = null");
        }
        return gameManager.setWood(woodCost);
    }

    public override void nextTurn()
    {
        if (!build && puppets.Count >= slotsToBuild)
        {
            buildProgress += 1;
            if (buildProgress >= timeToBuild)
            {
                build = true;
                changeAnimationTobuild();
            }
        }
        
        if (build)
        {
            // decrease hygiene here.
            gameManager.setFood(foodPerPuppet * puppets.Count);
        }
        lastTurn = gameManager.turnNumber;
    }
}
