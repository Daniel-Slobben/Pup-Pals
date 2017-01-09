using UnityEngine;
using System.Collections;

public class Workshop : Building {

    public Sprite workshop;

    public int timeToBuild;
    public static int woodCost = -12;
    public int slotsToBuild;
    public int woodPerPuppet;
    public float hygieneDecrease;
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

        GetComponent<SpriteRenderer>().sprite = construction;
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
            gameManager.setWood(woodPerPuppet * puppets.Count);
            Debug.Log("Awarded: " + woodPerPuppet * puppets.Count + " wood");
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

    /**
     * This method doesnt work yet because puppets dont have support for hygiene
     */
    private void decreaseHygieneOfPuppet()
    {
        foreach (GameObject puppet in puppets)
        {
            //float totalHygiene = puppet.getHygiene();
            //puppet.setHygiene((hygieneDecrease - 1) * totalHygiene);
        }
    }

    
}
