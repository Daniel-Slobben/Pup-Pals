using UnityEngine;
using System.Collections;
using System;

public class SanitationBuilding : Building
{

    public new int timeToBuild;
    public static int woodCost = -24;
    public new int slotsToBuild;

    private GameObject secondOldestPuppet = null;
    private GameObject oldestPuppet = null;

    // Use this for initialization
    // this building is not finished yet. In the start function it should make the animation change to construction animation
    new void Start()
    {
        base.Start();

        puppets = new ArrayList(slots);

        buildProgress = 0;

        GetComponent<SpriteRenderer>().sprite = construction;
    }

    public override bool cost()
    {
        if (gameManager == null)
        {
            Debug.Log("GameManager isnt set yet");
        }
        return gameManager.setWood(woodCost);
    }

    private void OnDestroy()
    {
        /*
        oldestPuppet.removeSanitize();
        secondOldestPuppet.removeSanitize();
        */
    }

    protected override void specialBuildingAction()
    {
        Debug.Log("Puppet is getting washed");
        foreach (GameObject puppet in puppets)
        {
            PuppetManager puppetScript = puppet.GetComponent<PuppetManager>();
            PuppetManager oldestPuppetScript = oldestPuppet.GetComponent<PuppetManager>();
            PuppetManager secondOldestPuppetScript = secondOldestPuppet.GetComponent<PuppetManager>();

            if (oldestPuppet == null)
            {
                oldestPuppet = puppet;
            }
            else if (secondOldestPuppet == null)
            {
                secondOldestPuppet = puppet;
            }
            else if (puppetScript.getAge() > oldestPuppetScript.getAge())
            {
                secondOldestPuppet = oldestPuppet;
                oldestPuppet = puppet;
            }
            else if (puppetScript.getAge() > secondOldestPuppetScript.getAge())
            {
                secondOldestPuppet = puppet;
            }
        }
        PuppetManager oldestPuppetScriptFinal = oldestPuppet.GetComponent<PuppetManager>();
        PuppetManager secondOldestPuppetScriptFinal = secondOldestPuppet.GetComponent<PuppetManager>();
        oldestPuppetScriptFinal.sanitize();
        secondOldestPuppetScriptFinal.sanitize();
    }
}
