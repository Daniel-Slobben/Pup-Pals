using UnityEngine;
using System.Collections;

public class Farm : Building {
    
    public int timeToBuild;
    public static int woodCost = -12;
    public int slotsToBuild;

    public Sprite construction;
    public Sprite farm;

    public new int slots;

    public string state; // 4 states: None, Sowing, Growing, Harvest

    private int buildProgress; // if this reaches timeToBuild the building is done
    private int lastTurn;
    private bool build;
    

	// Use this for initialization
	new void Start () {
        Debug.Log("atleast i tried");
        base.Start();
        Debug.Log("money cost" + gameManager.buildingMaterials);

        puppets = new ArrayList(slots);

        GetComponent<SpriteRenderer>().sprite = construction;

        buildProgress = 0;
        lastTurn = gameManager.turnNumber;
	}
	
	// Update is called once per frame
	void Update () {
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
                changeAnimation();
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
    /**
     * Should be changed to an animation
     */
    private void changeAnimation()
    {
        GetComponent<SpriteRenderer>().sprite = farm;
    }
}
