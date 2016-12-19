using UnityEngine;
using System.Collections;
using System;

public class SanitationBuilding : Building
{
    public Sprite sanitationBuilding;

    public int timeToBuild;
    public static int woodCost = -24;
    public int slotsToBuild;

    public new int slots;

    private int buildProgress; // if this reaches timeToBuild the building is done
    private int lastTurn;
    private bool build;

    private GameObject secondOldestPuppet = null;
    private GameObject oldestPuppet = null;

    // Use this for initialization
    // this building is not finished yet. In the start function it should make the animation change to construction animation
    new void Start()
    {
        base.Start();

        puppets = new ArrayList(slots);

        buildProgress = 0;
        lastTurn = gameManager.turnNumber;

        GetComponent<SpriteRenderer>().sprite = sanitationBuilding;
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
            lastTurn = gameManager.turnNumber;
            sanitizePuppets();
        }
        if (!build && puppets.Count >= slotsToBuild)
        {
            if (buildProgress < timeToBuild)
            {
                // The building is not done yet;
            }
            else
            {
                build = true;
                changeAnimationTobuild();
            }
        }
    }
    public override bool cost()
    {
        if (gameManager == null)
        {
            Debug.Log("GameManager isnt set yet");
        }
        return gameManager.setWood(woodCost);
    }
    
    private void sanitizePuppets()
    {
        /*
        foreach(GameObject puppet in puppets)
        {
            if (oldestPuppet == null)
            {
                oldestPuppet = puppet;
            }
            else if (secondOldestPuppet == null)
            {
                secondOldestPuppet = puppet;
            }
            else if (puppet.getAge() > oldestPuppet.getAge())
            {
                secondOldestPuppet = oldestPuppet;
                oldestPuppet = puppet;
            }
            else if (puppet.getAge() > secondOldestPuppet.getAge())
            {
                secondOldestPuppet = puppet;
            }
        }
        oldestPuppet.sanitize();
        secondOldestPuppet.sanitize();
         */
    }

    private void OnDestroy()
    {
        /*
        oldestPuppet.removeSanitize();
        secondOldestPuppet.removeSanitize();
        */
    }
}
