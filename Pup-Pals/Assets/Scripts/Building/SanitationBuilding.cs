using UnityEngine;
using System.Collections;
using System;

public class SanitationBuilding : Building
{

    public new int timeToBuild;
    public static int woodCost = -24;
    public new int slotsToBuild;
    
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

    private void OnDestroy()
    {
        foreach(GameObject puppet in puppets)
        {
            PuppetManager script = puppet.GetComponent<PuppetManager>();
            script.sanitized = false;
        }
    }

    protected override void specialBuildingAction()
    {
        Debug.Log("Puppet is getting washed");


        GameObject secondOldestPuppet = null;
        GameObject oldestPuppet = null;

        foreach (GameObject puppet in puppets)
        {
            PuppetManager puppetScript = puppet.GetComponent<PuppetManager>();
            PuppetManager oldestPuppetScript = null;
            PuppetManager secondOldestPuppetScript = null;

            if (oldestPuppet != null)
            {
                oldestPuppetScript = oldestPuppet.GetComponent<PuppetManager>();
            }
            if (secondOldestPuppet != null)
            {
                secondOldestPuppetScript = secondOldestPuppet.GetComponent<PuppetManager>();
            }            

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

        if (oldestPuppet != null)
        {
            PuppetManager oldestPuppetScriptFinal = oldestPuppet.GetComponent<PuppetManager>();
            oldestPuppetScriptFinal.sanitize();
        }
        if (secondOldestPuppet != null)
        {
            PuppetManager secondOldestPuppetScriptFinal = secondOldestPuppet.GetComponent<PuppetManager>();
            secondOldestPuppetScriptFinal.sanitize();
        }
                
    }
}
