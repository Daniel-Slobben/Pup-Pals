using UnityEngine;
using System.Collections;
using System;

public class Workshop : Building {

    public Sprite workshop;
    
    public static int woodCost = -12;
    public int woodPerPuppet;
    public float hygieneDecrease;

    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    public override bool cost()
    {
        if (gameManager == null)
        {
            // GameManager isnt set yet.
        }
        return gameManager.setWood(woodCost);
    }

    protected override void specialBuildingAction()
    {
        //not needed
    }
}
