/*
 * This script handles the tutorial.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 23-01-2017
 */


using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour {
    public Text tutorialText;
    public string textPhase;
    public Camera camera;
    public int tutorialInt;

    public GameObject farm;
    public GameObject buildingPlatform;
    public GameObject pup;
    public Button missionButton;
    public Button nextTurnButton;
    public Button puppetButton;

    //All the arrows, instantiated in the inspector to keep them at the good place.
    public Transform arrow1;
    public Transform arrow2;
    public Transform arrow3;
    public Transform arrow4;
    public Transform arrow5;
    public Transform arrow6;
    public Transform arrow7;
    public Transform arrow8;
    public Transform arrow9;
    public Transform arrow10;
    public Transform arrow11;
    public Transform arrow12;
    public Transform arrow13;
    public void Update()
    {
        tutorialText.text = "" + textPhase;
    }
    public void startTutorial()
    {
        tutorialInt = 0;
        camera.GetComponent<CameraMovement>().enabled = false;
        missionButton.interactable = false;
        nextTurnButton.interactable = false;
        puppetButton.interactable = false;
        GetComponent<GameManager>().showEvents = false;
        textPhase = "Welcome to PupPals! My name is Meo and i will help you get this village started!";
    }

    public void nextTutorial()
    {
        tutorialInt++;
        switch (tutorialInt)
        {
            case 1:
                textPhase = "Let's look around and see what we have!";
                break;
            case 2:
                arrow1.GetComponent<Renderer>().enabled = true;
                textPhase = "This is your resource bar, it shows how much food, wood and gold you have earned!";
                break;
            case 3:
                textPhase = "Creating new puppets or structures will cost resources, so keep an eye on this.";
                break;
            case 4:
                textPhase = "Your puppets will need some food every turn, so make sure there is enough for everyone!";
                break;
            case 5:
                arrow1.GetComponent<Renderer>().enabled = false;
                arrow2.GetComponent<Renderer>().enabled = true;
                textPhase = "This is a building-plot, it allows you to create structures!";
                break;
            case 6:
                textPhase = "Every building provides you with something useful once it is completed.";
                break;
            case 7:
                textPhase = "If puppets work at a farm it will provide food for your village.";
                break;
            case 8:
                textPhase = "If puppets work at a workshop they will provide wood.";
                break;
            case 9:
                textPhase = "Let's build a farm for your village together!";
                break;
            case 10:
                arrow2.GetComponent<Renderer>().enabled = false;
                farm.SetActive(true);
                arrow3.GetComponent<Renderer>().enabled = true;
                textPhase = "If you click on a empty building-plot a small menu will appear.";
                break;
            case 11:
                textPhase = "For now you can only build a farm. The amount of building-options will expand during the game!";
                break;
            case 12:
                arrow3.GetComponent<Renderer>().enabled = false;
                arrow4.GetComponent<Renderer>().enabled = true;
                buildingPlatform.GetComponent<BuildingPlot>().showBuildMenu("Farm");
                textPhase = "If you click on the icon of the building you want to build you can see how much resources it will cost.";
                break;
            case 13:
                arrow4.GetComponent<Renderer>().enabled = false;
                arrow5.GetComponent<Renderer>().enabled = true;
                buildingPlatform.GetComponent<BuildingPlot>().build("Farm");
                textPhase = "When you click on 'build' the structure will enter its construction phase.";
                break;
            case 14:
                textPhase = "Building will take several turns depending on the type of structure.";
                break;
            case 15:
                textPhase = "In order to complete the construction you need puppets, let's learn how to make puppets!";
                break;
            case 16:
                arrow5.GetComponent<Renderer>().enabled = false;
                arrow6.GetComponent<Renderer>().enabled = true;
                textPhase = "This button will let you create new puppets. Creating a new puppet will cost 20 wood and 20 food.";
                break;
            case 17:
                textPhase = "A farm needs 2 puppets to be build, lets create them!";
                GetComponent<GameManager>().addPuppet(pup);
                GetComponent<GameManager>().addPuppet(pup);
                break;
            case 18:
                textPhase = "As you can see there are two out of 6 puppets available right now.";
                arrow6.GetComponent<Renderer>().enabled = false;
                arrow7.GetComponent<Renderer>().enabled = true;
                break;
            case 19:
                textPhase = "If you 'left-click' on a puppet you select it, once selected it can be placed in a structure.";
                break;
            case 20:
                textPhase = "I have placed the puppets in the building plot for you, their portrait will change if the puppet is busy.";
                List<GameObject> puppets = GetComponent<GameManager>().puppets;
                GameObject farmclone = GameObject.Find("Farm(Clone)");
                foreach (GameObject puppet in puppets)
                {
                    farmclone.GetComponent<Farm>().addPuppet(puppet);
                }

                break;
            case 21:
                textPhase = "To remove them from their current job, 'right-click' on the puppet you whish to stop.";
                break;
            case 22:
                arrow7.GetComponent<Renderer>().enabled = false;
                arrow8.GetComponent<Renderer>().enabled = true;
                textPhase = "To make some building progress we are going to fastforward to the next day, this can be done by pressing this button.";
                break;
            case 23:
                textPhase = "Building a farm costs 4 days, I will fastforward it for you.";
                GetComponent<GameManager>().nextTurn();
                GetComponent<GameManager>().nextTurn();
                GetComponent<GameManager>().nextTurn();
                GetComponent<GameManager>().nextTurn();
                break;
            case 24:
                arrow8.GetComponent<Renderer>().enabled = false;
                arrow9.GetComponent<Renderer>().enabled = true;
                textPhase = "As you can see the farm has been completed, and the puppets are working the fields.";
                break;
            case 25:
                arrow9.GetComponent<Renderer>().enabled = false;
                arrow10.GetComponent<Renderer>().enabled = true;
                arrow11.GetComponent<Renderer>().enabled = true;
                textPhase = "Working in the village will make your puppets dirty. This 'dot' will show the hygiene level of your pet.";
                break;
            case 26:
                textPhase = "A bad hygiene level will make your puppets sick, and they can eventually break down...";
                break;
            case 27:
                textPhase = "A sick puppet will have consequences for the whole village.";
                break;
            case 28:
                textPhase = "To make your puppets healthy and clean again you can wash them!";
                break;
            case 29:
                arrow10.GetComponent<Renderer>().enabled = false;
                arrow11.GetComponent<Renderer>().enabled = false;
                arrow12.GetComponent<Renderer>().enabled = true;
                textPhase = "To wash your puppets you can click this soap bar.";
                break;
            case 30:
                arrow12.GetComponent<Renderer>().enabled = false;
                textPhase = "Sometimes puppets wander off and will find some resources, you can collect these resources by starting a gathering mission.";
                break;
            case 31:
                arrow13.GetComponent<Renderer>().enabled = true;
                textPhase = "This can be done by clicking this mission button, it will give you all the information about the options you have.";
                break;
            case 32:
                textPhase = "A mission has a certain succes chance and a reward, so keep an eye out for valuable missions!";
                break;
            case 33:
                arrow13.GetComponent<Renderer>().enabled = false;
                textPhase = "I have to help other villages now, I hope I helped you enough to get going!";
                break;
            case 34:
                arrow13.GetComponent<Renderer>().enabled = false;
                textPhase = "Good luck and keep your puppets clean! Hygiene is key to survival!";
                break;
            case 35:
                camera.GetComponent<CameraMovement>().enabled = true;
                missionButton.interactable = true;
                nextTurnButton.interactable = true;
                puppetButton.interactable = true;
                GetComponent<GameManager>().showEvents = true;
                GameObject.Find("TutorialPanel").SetActive(false);
                break;

        }
    }

}
