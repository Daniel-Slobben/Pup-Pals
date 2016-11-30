using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int soap = 0;
    public int buildingMaterials = 0;
    public int money = 0;
    public int turnNumber = 0;

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

    }
	
	// Update is called once per frame
	void Update () {
    }

    //Is called when the "End turn" button is pressed.
    public void nextTurn()
    {
        soap = soap + 5;
        turnNumber = turnNumber + 1;
        buildingMaterials = buildingMaterials + 3;
        money = money + 5;

        soapText.text = "" + soap;
        buildingMaterialsText.text = "" + buildingMaterials;
        moneyText.text = "" + money;
        turnNumberText.text = "" + turnNumber;

    }
}
