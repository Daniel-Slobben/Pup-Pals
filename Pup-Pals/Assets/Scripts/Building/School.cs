using UnityEngine;
using System.Collections;
using System;

public class School : Building {
    
    public static int woodCost = -32;
    public int timeToSchool;

    // Use this for initialization
    // this building is not finished yet. In the start function it should make the animation change to construction animation
    new void Start()
    {
        base.Start();
    }
    public override bool cost()
    {
        if (gameManager == null)
        {
            Debug.Log("GameManager isnt set yet");
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

