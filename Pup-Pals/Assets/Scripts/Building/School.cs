using UnityEngine;
using System.Collections;
using System;

public class School : Building {
    
    public static int woodCost = -32;
    public int timeToSchool;

    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    public override bool cost()
    {
        if (gameManager == null)
        {
            // GameManager isnt set yet
        }
        return gameManager.setWood(woodCost);
    }

    protected override void specialBuildingAction()
    {
        foreach (GameObject puppet in puppets)
        {
            PuppetManager puppetScript = puppet.GetComponent<PuppetManager>();
            puppetScript.timeInSchool++;
            if (puppetScript.timeInSchool > timeToSchool)
            {
                puppetScript.school();
            }
        }
    }
}

