/*
 * This script handles the next button of the tutorial.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 23-01-2017
 */

using UnityEngine;
using System.Collections;

public class nextTutorialPhase : MonoBehaviour {
	
    public void nextPhase()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        TutorialManager tutorialManager = gameManager.GetComponent<TutorialManager>();
        tutorialManager.nextTutorial();
    }

}
