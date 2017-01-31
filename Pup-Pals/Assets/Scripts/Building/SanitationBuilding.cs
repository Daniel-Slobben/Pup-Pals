using UnityEngine;
using System.Collections;
using System;

public class SanitationBuilding : Building
{
    public static int woodCost = -24;

    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    public override bool cost()
    {
        if (gameManager == null)
        {
            // GameManager isnt set.
        }
        return gameManager.setWood(woodCost);
    }

    private void OnDestroy()
    {
        foreach (GameObject puppet in puppets)
        {
            PuppetManager script = puppet.GetComponent<PuppetManager>();
            script.sanitized = false;
        }
    }

    protected override void specialBuildingAction()
    {
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
            oldestPuppetScriptFinal.sick = false;
            oldestPuppetScriptFinal.hygiene = 100;
        }
        if (secondOldestPuppet != null)
        {
            PuppetManager secondOldestPuppetScriptFinal = secondOldestPuppet.GetComponent<PuppetManager>();
            secondOldestPuppetScriptFinal.sanitize();
            secondOldestPuppetScriptFinal.sick = false;
            secondOldestPuppetScriptFinal.hygiene = 100;
        }

    }
}
