using UnityEngine;
using System.Collections;

public class School : Building {

    public Sprite school;

    public int timeToBuild;
    public static int woodCost = -32;
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

        GetComponent<SpriteRenderer>().sprite = school;
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
            Debug.Log("GameManager isnt set yet");
        }
        return gameManager.setWood(woodCost);
    }
}

