using UnityEngine;
using System.Collections;
using System;

public class House : Building {

    public Sprite house;

    public int timeToBuild;
    public int woodCost;
    public int slotsToBuild;

    public new int slots;

    private int buildProgress; // if this reaches timeToBuild the building is done
    private int lastTurn;
    private bool build;


    // Use this for initialization
    // this building is not finished yet. In the start function it should make the animation change to construction animation
    new void Start()
    {
        base.Start();

        puppets = new ArrayList(slots);

        buildProgress = 0;
        lastTurn = gameManager.turnNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastTurn != gameManager.turnNumber)
        {
            if (!build && puppets.Count >= slotsToBuild)
            {
                buildProgress += 1;
            }
            // decrease hygiene here.
            lastTurn = gameManager.turnNumber;
        }
        if (!build && puppets.Count >= slotsToBuild)
        {
            if (buildProgress < timeToBuild)
            {
                // not done yet
            }
            else
            {
                build = true;
                // change animation here
            }
        }
    }
    public override bool cost()
    {
        if (gameManager == null)
        {
            Debug.Log("null gamiemanager");
        }
        return gameManager.setWood(woodCost);
    }
}
