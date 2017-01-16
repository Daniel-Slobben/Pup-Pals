using UnityEngine;
using System.Collections;
using System;

public class Workshop : Building {

    public Sprite workshop;

    public new int timeToBuild;
    public static int woodCost = -12;
    public new int slotsToBuild;
    public int woodPerPuppet;
    public float hygieneDecrease;

    // Use this for initialization
    // this building is not finished yet. In the start function it should make the animation change to construction animation
    new void Start()
    {
        base.Start();

        puppets = new ArrayList(slots);

        buildProgress = 0;
    }

    public override bool cost()
    {
        if (gameManager == null)
        {
            Debug.Log("GameManager isnt set yet");
        }
        return gameManager.setWood(woodCost);
    }

    protected override void specialBuildingAction()
    {
        //throw new NotImplementedException();
    }
}
