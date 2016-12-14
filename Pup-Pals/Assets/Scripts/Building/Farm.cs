using UnityEngine;
using System.Collections;

public class Farm : Building {
    
    public int timeToBuild;
    public int woodCost;
    public int slotsToBuild;

    public Sprite construction;
    public Sprite farm;

    public new int slots;

    public string state; // 4 states: None, Sowing, Growing, Harvest

    private int buildProgress; // if this reaches timeToBuild the building is done
    private int lastTurn;
    private bool build;
    

	// Use this for initialization
	void Start () {
        // cost 
        //GameManager.buildingMaterials -= woodCost;

        puppets = new ArrayList(slots);

        GetComponent<SpriteRenderer>().sprite = construction;

        buildProgress = 0;
        lastTurn = GameManager.turnNumber;
	}
	
	// Update is called once per frame
	void Update () {
        if (lastTurn != GameManager.turnNumber)
        {
            if (!build && puppets.Count >= slotsToBuild)
            {
                buildProgress += 1;
            }
            // decrease hygiene here.
            lastTurn = GameManager.turnNumber;
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
    /**
     * Should be changed to an animation
     */
    private void changeAnimation()
    {
        GetComponent<SpriteRenderer>().sprite = farm;
    }
}
