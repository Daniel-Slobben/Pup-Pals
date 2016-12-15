using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int soap = SaveLoadController.control.food;
    public int buildingMaterials = SaveLoadController.control.buldingMaterials;
    public int money = SaveLoadController.control.money;
    public int turnNumber = SaveLoadController.control.turnNumber;

    public ArrayList puppets;

    Text soapText;
    Text buildingMaterialsText;
    Text moneyText;
    Text turnNumberText;

    // Use this for initialization
    void Start () {

        soapText = GameObject.Find("SoapValue").GetComponent<Text>();
        buildingMaterialsText = GameObject.Find("BuildMatsValue").GetComponent<Text>();
        moneyText = GameObject.Find("MoneyValue").GetComponent<Text>();
        turnNumberText = GameObject.Find("TurnValue").GetComponent<Text>();

        soapText.text = "" + SaveLoadController.control.food;
        buildingMaterialsText.text = "" + SaveLoadController.control.buldingMaterials;
        moneyText.text = "" + SaveLoadController.control.money;
        turnNumberText.text = "" + SaveLoadController.control.turnNumber;

        soap = SaveLoadController.control.food;
        buildingMaterials = SaveLoadController.control.buldingMaterials;
        money = SaveLoadController.control.money;
        turnNumber = SaveLoadController.control.turnNumber;

        puppets = new ArrayList(6);
    }
	
	// Update is called once per frame
	void Update () {
    }

    //Is called when the "End turn" button is pressed.
    public void nextTurn()
    {
        setFood(5);
        setWood(3);
        setMoney(5);

        turnNumber = turnNumber + 1;
        SaveLoadController.control.turnNumber += 1;
        turnNumberText.text = "" + SaveLoadController.control.turnNumber;
    }

    public void addPuppet(GameObject puppet)
    {
        if (puppets.Count < 6)
        {
            puppets.Add(puppet);
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
        if (soap + amountOfFood < 0)
        {
            return false;
        }
        soap += amountOfFood;
        SaveLoadController.control.food += amountOfFood;
        soapText.text = "" + SaveLoadController.control.food;
        return true;
    }
    public bool setWood(int amountOfWood)
    {
        if (buildingMaterials + amountOfWood < 0)
        {
            return false;
        }
        buildingMaterials += amountOfWood;
        SaveLoadController.control.buldingMaterials += amountOfWood;
        buildingMaterialsText.text = "" + SaveLoadController.control.buldingMaterials;
        return true;
    }
    public bool setMoney(int AmountOfMoney)
    {
        if (money + AmountOfMoney < 0)
        {
            return false;
        }
        money += AmountOfMoney;
        SaveLoadController.control.money += AmountOfMoney;
        moneyText.text = "" + SaveLoadController.control.money;
        return true;
    }
}
