/*
 * This script creates random generated events.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 13-01-2017
 */

using UnityEngine;
using System.Collections;

public class Events {

    private GameManager gameManager;

    public Events(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void rollEvent()
    {
        preRoll();
        int randomNumber = Random.Range(1, 100);

        if (randomNumber <= 50)
        {
            return;
        }
        // Lightning Strike
        if (randomNumber > 50 && randomNumber <= 55)
        {
            // rip puppet
        }
        // Thief
        if (randomNumber > 55 && randomNumber <= 60)
        {
            gameManager.setFood(-10);
            gameManager.showEventPanel("A thief stole your 10 of your food!");
            return;
        }
        // Fire
        if (randomNumber > 60 && randomNumber <= 65)
        {
            gameManager.setWood(-10);
            gameManager.showEventPanel("A fire burned 10 of your wood!");
            return;
        }
        // Flood
        if (randomNumber > 65 && randomNumber <= 70)
        {
            // All farms that do not have the flood wall upgrade have their growing phase reset to 0/2 sowing
            return;
        }
        // Wind storm
        if (randomNumber > 70 && randomNumber <= 75)
        {
            // Destroy one random building without the sturdy walls upgrade
            return;
        }
        // Traveling Shop
        if (randomNumber > 75 && randomNumber <= 80)
        {
            // Traveling shop appears in the village until the next event triggers
            return;
        }
        // Food poisoning
        if (randomNumber > 90 && randomNumber <= 85)
        {
            foreach (GameObject puppet in gameManager.puppets)
            {
                PuppetManager puppetScript = puppet.GetComponent<PuppetManager>();
                if (!puppetScript.sick)
                {
                    puppetScript.sick = true;
                    gameManager.showEventPanel("One of your puppets got sick!");
                    // puppet is sick
                    break;
                }
            }
            return;
        }

        // Gift
        if (randomNumber > 85 && randomNumber <= 90)
        {
            gameManager.showEventPanel("You have received 5 food and 5 wood!");
            gameManager.setFood(5);
            gameManager.setWood(5);
            return;
        }
        // Wanderer
        if (randomNumber > 90 && randomNumber <= 95)
        {
            //Player gets a free new puppet.Puppet type is random from those unlocked.If player has max number of puppets, no effect.
            return;
        }
        // Heavy rain
        if (randomNumber > 95 && randomNumber <= 100)
        {
            foreach (GameObject puppet in gameManager.puppets)
            {
                PuppetManager puppetScript = puppet.GetComponent<PuppetManager>();
                if (!puppetScript.sanitized)
                {
                    puppetScript.muddy = true;
                }
            }
            return;
        }
    }
    private void preRoll()
    {
        foreach (GameObject puppet in gameManager.puppets)
        {
            PuppetManager puppetScript = puppet.GetComponent<PuppetManager>();
            puppetScript.muddy = false;
        }
    }
}
