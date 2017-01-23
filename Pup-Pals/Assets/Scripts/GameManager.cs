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

    public List<GameObject> puppets;
    public List<GameObject> puppetSlots;

    public static GameObject PuppetTransport;
    CreateMissions createMissionScript;

    public ArrayList buildings;

    public bool firstPlay;

    public GameObject GUI;
    public GameObject eventPanel;
    public GameObject gameOverScreen;

    private Events events;

    Text foodText;
    Text buildingMaterialsText;
    Text moneyText;
    Text turnNumberText;
    Text text;

    // Use this for initialization
    void Start()
    {
        events = new Events(this);

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

    // Update is called once per frame
    void Update()
    {
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
            if(puppet.GetComponent<PuppetManager>().onMission == true)
            {
                puppet.GetComponent<PuppetManager>().checkMissionStatus();
            }
        }
        checkIfGameIsWon();
    }

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

                //puppetSlots[puppetId].SetActive(true);
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
            Debug.Log("No slots available");
        }
    }

    public void removePuppet(GameObject puppetToRemove)
    {
        Debug.Log("Removing puppet..");
        foreach (GameObject puppet in puppets)
        {
            if (puppetToRemove == puppet)
            {
                PuppetManager puppetManager = puppet.GetComponent<PuppetManager>();
                //Change slot icon to empty
                puppets.Remove(puppetToRemove);
                Destroy(puppet);
                return;
            }
        }
    }

    public bool setFood(int amountOfFood)
    {
        if (food + amountOfFood < 0)
        {
            // game should go dead
            gameOverScreen.SetActive(true);
            return false;
        }
        food += amountOfFood;
        //SaveLoadController.control.food += amountOfFood;
        foodText.text = "" + food;
        return true;
    }
    public bool setWood(int amountOfWood)
    {
        if (buildingMaterials + amountOfWood < 0)
        {
            return false;
        }
        buildingMaterials += amountOfWood;
        //SaveLoadController.control.buldingMaterials += amountOfWood;
        buildingMaterialsText.text = "" + buildingMaterials;
        return true;
    }
    public bool setMoney(int AmountOfMoney)
    {
        if (money + AmountOfMoney < 0)
        {
            return false;
        }
        money += AmountOfMoney;
        //SaveLoadController.control.money += AmountOfMoney;
        moneyText.text = "" + money;
        return true;
    }
    private void notifyBuildingOfNextTurn()
    {
        foreach (Building building in buildings)
        {
            building.nextTurn();
        }
    }

    public void addBuilding(Building building)
    {
        if (!buildings.Contains(building))
        {
            buildings.Add(building);
        }
    }

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
    public void showEventPanel(string text)
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

    private void checkIfGameIsWon()
    {
        int counter = 0;
        foreach (GameObject puppet in puppets)
        {
            PuppetManager puppetScript = puppet.GetComponent<PuppetManager>();
            if (puppetScript.sanitized)
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
        // we win now boi
    }
}
