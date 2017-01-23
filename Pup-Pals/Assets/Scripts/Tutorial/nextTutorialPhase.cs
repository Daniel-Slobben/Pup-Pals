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
