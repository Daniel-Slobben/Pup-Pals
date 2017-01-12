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

    public ArrayList buildings;

    public bool firstPlay;

    Text foodText;
    Text buildingMaterialsText;
    Text moneyText;
    Text turnNumberText;
    Text text;

    // Use this for initialization
    void Start()
    {

        text = GameObject.Find("WelcomeText").GetComponent<Text>();


        foodText = GameObject.Find("FoodValue").GetComponent<Text>();
        buildingMaterialsText = GameObject.Find("BuildMatsValue").GetComponent<Text>();
        moneyText = GameObject.Find("MoneyValue").GetComponent<Text>();
        turnNumberText = GameObject.Find("TurnValue").GetComponent<Text>();

        int playerIdentity = SaveLoadController.control.getPlayerIdentity();
        text.GetComponentInChildren<Text>().text = SaveLoadController.control.getPlayerName(playerIdentity) + "'s village";

        text.CrossFadeAlpha(0.0f, 2.0f, false);

        if (firstPlay == false)
        {
            firstPlay = true;
            food = 100;
            buildingMaterials = 125;
            money = 50;

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
        //SaveLoadController.control.turnNumber += 1;
        turnNumberText.text = "" + turnNumber;

        notifyBuildingOfNextTurn();
        updatePuppets(puppets);
    }

    public void addPuppet(GameObject puppet)
    {
        if (puppets.Count < 6)
        {
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
            Debug.Log("Notifying buildings of next turn");
        }
    }

    public void addBuilding(Building building)
    {
        buildings.Add(building);
        Debug.Log("Added building to the game manager");
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
        foreach(GameObject slot in puppetSlots)
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
        foreach(GameObject puppet in puppets)
        {
            Debug.Log("ik zit nu in de foreach");
            PuppetManager puppetManager = puppet.GetComponent<PuppetManager>();
            GameObject slot = puppetSlots[puppetManager.puppetId];
            PuppetPanel puppetPanelScript = slot.GetComponent<PuppetPanel>();
            puppetPanelScript.puppetSlot = puppet;

        }
    }

}
