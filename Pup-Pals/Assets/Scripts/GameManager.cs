/*
 * This script handles the actions of every turn.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 16-12-2016
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public int food;
    public int buildingMaterials;
    public int money;
    public int turnNumber;

    public List<GameObject> puppets;        //List of all the instantiated puppet objects.
    public List<GameObject> puppetSlots;    //List of all the available puppetslots.

    public static GameObject PuppetTransport;
    CreateMissions createMissionScript;

    public ArrayList buildings;

    public bool firstPlay;
    public bool showEvents;
    public bool gameWon;

    public GameObject tutorialPanel;
    public GameObject GUI;
    public GameObject eventPanel;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public int i;                           //Int that is used for checking the tutorial after 5 frames.
    private Events events;

    Text foodText;
    Text buildingMaterialsText;
    Text moneyText;
    Text turnNumberText;
    Text text;

    //Instantiate all the text, create missions and get the player ID.
    void Start()
    {
        i = 0;
        events = new Events(this);
        showEvents = true;
        text = GameObject.Find("WelcomeText").GetComponent<Text>();
        foodText = GameObject.Find("FoodValue").GetComponent<Text>();
        buildingMaterialsText = GameObject.Find("BuildMatsValue").GetComponent<Text>();
        moneyText = GameObject.Find("MoneyValue").GetComponent<Text>();
        turnNumberText = GameObject.Find("TurnValue").GetComponent<Text>();

        createMissionScript = gameObject.GetComponent<CreateMissions>();

        int playerIdentity = SaveLoadController.control.getPlayerIdentity();
        text.GetComponentInChildren<Text>().text = SaveLoadController.control.getPlayerName(playerIdentity) + "'s village";

        text.CrossFadeAlpha(0.0f, 2.0f, false);

        if (firstPlay == false)
        {
            firstPlay = true;
            food = 130;
            buildingMaterials = 100;
            money = 10;
            puppets = new List<GameObject>(6);
            buildings = new ArrayList();

        }

        Invoke("checkSlots", 1);
    }
    //Check if tutorial has to be launched, update text and check if the cursor has to be set.
    void Update()
    {

        if (i <= 5)
        {
            i++;
            if (i == 5)
            {
                checkTutorial(false);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            GameManager.PuppetTransport = null;
        }

        foodText.text = "" + food;
        buildingMaterialsText.text = "" + buildingMaterials;
        moneyText.text = "" + money;
        turnNumberText.text = "" + turnNumber;
    }

    //Is called when the "End turn" button is pressed.
    public void nextTurn()
    {

        turnNumber = turnNumber + 1;
        turnNumberText.text = "" + turnNumber;
        events.rollEvent();
        notifyBuildingOfNextTurn();
        updatePuppets(puppets);

        if (turnNumber % 3 == 0)
        {
            createMissionScript.generateMission();
        }

        foreach (GameObject puppet in puppets)
        {
            if (puppet.GetComponent<PuppetManager>().onMission == true)
            {
                puppet.GetComponent<PuppetManager>().checkMissionStatus();
            }
        }
        checkIfGameIsWon();
    }

    //Creates a new puppet
    public void addPuppet(GameObject puppet)
    {
        if (puppets.Count < 6)
        {
            if (buildingMaterials >= 20 && food >= 20)
            {
                setWood(-20);
                setFood(-20);
                int puppetId = findEmptyPuppetSlot();
                GameObject newPuppet = Instantiate(puppet);
                PuppetManager puppetManager = newPuppet.GetComponent<PuppetManager>();
                puppetManager.puppetId = puppetId;
                PuppetPanel slotScript = (PuppetPanel)puppetSlots[puppetId].GetComponent(typeof(PuppetPanel));
                slotScript.puppetSlot = newPuppet;
                puppets.Add(newPuppet);
            }
            else
            {
                showEventPanel("You don't have enough resources to create a puppet!");
                if (puppets.Count == 0)
                {
                    gameOverScreen.SetActive(true);
                }
            }

        }
        else
        {
            showEventPanel("There are no empty slots available!");
        }
    }

    //Removes the puppet
    public void removePuppet(GameObject puppetToRemove)
    {
        foreach (GameObject puppet in puppets)
        {
            if (puppetToRemove == puppet)
            {
                PuppetManager puppetManager = puppet.GetComponent<PuppetManager>();
                puppets.Remove(puppetToRemove);
                Destroy(puppet);
                return;
            }
        }
    }

    //Sets amount of food.
    public bool setFood(int amountOfFood)
    {
        if (food + amountOfFood < 0)
        {
            gameOverScreen.SetActive(true);
            return false;
        }
        food += amountOfFood;
        foodText.text = "" + food;
        return true;
    }
    
    //Sets amount of wood.
    public bool setWood(int amountOfWood)
    {
        if (buildingMaterials + amountOfWood < 0)
        {
            return false;
        }
        buildingMaterials += amountOfWood;
        buildingMaterialsText.text = "" + buildingMaterials;
        return true;
    }

    //Sets amount of gold.
    public bool setMoney(int AmountOfMoney)
    {
        if (money + AmountOfMoney < 0)
        {
            return false;
        }
        money += AmountOfMoney;
        moneyText.text = "" + money;
        return true;
    }

    //Updates building stats.
    private void notifyBuildingOfNextTurn()
    {
        foreach (Building building in buildings)
        {
            building.nextTurn();
        }
    }

    //Adds new building.
    public void addBuilding(Building building)
    {
        if (!buildings.Contains(building))
        {
            buildings.Add(building);
        }
    }


    //Removes building.
    public void removeBuilding(Building buildingToRemove)
    {
        foreach (Building building in buildings)
        {
            if (buildingToRemove == building)
            {
                buildings.Remove(buildingToRemove);
                return;
            }
        }
    }

    //Updates every puppet.
    public void updatePuppets(List<GameObject> puppets)
    {
        List<GameObject> tempPuppets;
        tempPuppets = puppets;


        foreach (GameObject puppet in tempPuppets.ToArray())
        {
            PuppetManager puppetManager = puppet.GetComponent<PuppetManager>();
            if (puppetManager.notifyPuppetEndTurn())
            {
                removePuppet(puppet);
            }
        }

    }

    //Checks for empty puppetslots.
    public int findEmptyPuppetSlot()
    {
        foreach (GameObject slot in puppetSlots)
        {
            PuppetPanel puppetPanel = slot.GetComponent<PuppetPanel>();
            if (puppetPanel.puppetSlot == null)
            {
                return puppetPanel.slotId;
            }

        }
        return 0;
    }


    public void checkSlots()
    {
        foreach (GameObject puppet in puppets)
        {
            PuppetManager puppetManager = puppet.GetComponent<PuppetManager>();
            GameObject slot = puppetSlots[puppetManager.puppetId];
            PuppetPanel puppetPanelScript = slot.GetComponent<PuppetPanel>();
            puppetPanelScript.puppetSlot = puppet;

        }
    }

    //Popup panel for event information.
    public void showEventPanel(string text)
    {
        if (showEvents = true)
        {
            GameObject panel = Instantiate(eventPanel);
            panel.transform.parent = GUI.transform;
            Vector2 position = new Vector2(814, 136.32f);
            panel.GetComponent<RectTransform>().anchoredPosition = position;
            GameObject panelChild = panel.transform.GetChild(0).gameObject;
            panelChild.GetComponent<Text>().text = text;
            panel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);


            panel.SetActive(true);
        }
    }

    //Double check for the tutorial launch.
    public void checkTutorial(bool firstPlay)
    {
        if (firstPlay == false)
        {
            firstPlay = true;
            if (turnNumber <= 0)
            {
                TutorialManager tman = GetComponent<TutorialManager>();
                tutorialPanel.SetActive(true);
                tman.startTutorial();
            }
        }

    }

    private void checkIfGameIsWon()
    {
        int counter = 0;
        foreach (GameObject puppet in puppets)
        {
            PuppetManager puppetScript = puppet.GetComponent<PuppetManager>();
            if (puppetScript.schooled)
            {
                counter++;
            }
            if (counter >= 4)
            {
                winState();
            }

        }
    }
    private void winState()
    {
        gameWon = true;
        winScreen.SetActive(true);
    }
}
