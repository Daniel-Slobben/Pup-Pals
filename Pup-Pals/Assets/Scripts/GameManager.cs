using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public int food;
    public int buildingMaterials;
    public int money;
    public int turnNumber;

    public ArrayList puppets;
    public List<GameObject> puppetSlots;

    public static GameObject PuppetTransport;

    Text foodText;
    Text buildingMaterialsText;
    Text moneyText;
    Text turnNumberText;
    Text text;

    // Use this for initialization
    void Start () {
        
        text = GameObject.Find("WelcomeText").GetComponent<Text>();
        

        foodText = GameObject.Find("FoodValue").GetComponent<Text>();
        buildingMaterialsText = GameObject.Find("BuildMatsValue").GetComponent<Text>();
        moneyText = GameObject.Find("MoneyValue").GetComponent<Text>();
        turnNumberText = GameObject.Find("TurnValue").GetComponent<Text>();

        int playerIdentity = SaveLoadController.control.getPlayerIdentity();
        text.GetComponentInChildren<Text>().text = SaveLoadController.control.getPlayerName(playerIdentity) + "'s village";

        foodText.text = "" + food;
        buildingMaterialsText.text = "" + buildingMaterials;
        moneyText.text = "" + money;
        turnNumberText.text = "" + turnNumber;
        text.CrossFadeAlpha(0.0f, 2.0f, false);

        puppets = new ArrayList(6);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            GameManager.PuppetTransport = null;
        }
    }

    //Is called when the "End turn" button is pressed.
    public void nextTurn()
    {
        setFood(5);
        setWood(3);
        setMoney(5);

        turnNumber = turnNumber + 1;
        //SaveLoadController.control.turnNumber += 1;
        turnNumberText.text = "" + turnNumber;
    }

    public void addPuppet(GameObject puppet)
    {
        if (puppets.Count < 6)
        {
            GameObject newPuppet = Instantiate(puppet);
            puppetSlots[puppets.Count].SetActive(true);
            PuppetPanel slotScript = (PuppetPanel)puppetSlots[puppets.Count].GetComponent(typeof(PuppetPanel));
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
        foreach (GameObject puppet in puppets)
        {
            if (puppetToRemove == puppet)
            {
                puppets.Remove(puppetToRemove);
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
}
