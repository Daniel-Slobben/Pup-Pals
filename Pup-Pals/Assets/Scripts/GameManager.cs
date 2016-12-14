using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int soap = SaveLoadController.control.food;
    public int buildingMaterials = 0;
    public int money = 0;
    public static int turnNumber = 0;

    public ArrayList puppets;

    Text soapText;
    Text buildingMaterialsText;
    Text moneyText;
    Text turnNumberText;

    // Use this for initialization
    void Start () {
        Debug.Log("new managar made");
        soapText = GameObject.Find("SoapValue").GetComponent<Text>();
        buildingMaterialsText = GameObject.Find("BuildMatsValue").GetComponent<Text>();
        moneyText = GameObject.Find("MoneyValue").GetComponent<Text>();
        turnNumberText = GameObject.Find("TurnValue").GetComponent<Text>();

        soapText.text = "" + SaveLoadController.control.food;
        buildingMaterialsText.text = "" + SaveLoadController.control.buldingMaterials;
        moneyText.text = "" + SaveLoadController.control.money;
        turnNumberText.text = "" + SaveLoadController.control.turnNumber;

        puppets = new ArrayList(6);

    }
	
	// Update is called once per frame
	void Update () {
    }

    //Is called when the "End turn" button is pressed.
    public void nextTurn()
    {
        soap = soap + 5;
        SaveLoadController.control.food += 5;

        turnNumber = turnNumber + 1;
        SaveLoadController.control.turnNumber += 1;

        buildingMaterials = buildingMaterials + 3;
        SaveLoadController.control.buldingMaterials += 3;

        money = money + 5;
        SaveLoadController.control.money += 5;

        soapText.text = "" + SaveLoadController.control.food;
        buildingMaterialsText.text = "" + SaveLoadController.control.buldingMaterials;
        moneyText.text = "" + SaveLoadController.control.money;
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
}
