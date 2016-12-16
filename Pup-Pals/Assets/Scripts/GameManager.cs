using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject puppetPrefab;

    public int soap = SaveLoadController.control.food;
    public int buildingMaterials = SaveLoadController.control.buldingMaterials;
    public int money = SaveLoadController.control.money;
    public int turnNumber = SaveLoadController.control.turnNumber;
    public ArrayList puppets;
    public ArrayList puppetsSerializable;

    // sprites of all the puppets we rebuild
    public Sprite puppetSprite1; // should be sprite Good_Cat. in the puppet script it should have name 1

    Text soapText;
    Text buildingMaterialsText;
    Text moneyText;
    Text turnNumberText;

    // Use this for initialization
    void Start () {
        this.puppetsSerializable = SaveLoadController.control.puppets;
        soapText = GameObject.Find("SoapValue").GetComponent<Text>();
        buildingMaterialsText = GameObject.Find("BuildMatsValue").GetComponent<Text>();
        moneyText = GameObject.Find("MoneyValue").GetComponent<Text>();
        turnNumberText = GameObject.Find("TurnValue").GetComponent<Text>();

        soap = SaveLoadController.control.food;
        buildingMaterials = SaveLoadController.control.buldingMaterials;
        money = SaveLoadController.control.money;
        turnNumber = SaveLoadController.control.turnNumber;

        soapText.text = "" + SaveLoadController.control.food;
        buildingMaterialsText.text = "" + SaveLoadController.control.buldingMaterials;
        moneyText.text = "" + SaveLoadController.control.money;
        turnNumberText.text = "" + SaveLoadController.control.turnNumber;

        
        puppets = new ArrayList(6);

        
        deSerializePuppets();

        puppets.Add(Instantiate(puppetPrefab));
        Debug.Log("Puppet count = now: " + puppets.Count);
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
    public void serializePuppets()
    {
        puppetsSerializable.Clear();
        foreach (GameObject puppet in puppets)
        {
            ArrayList puppetSerialized = new ArrayList();
            Vector3 puppetPosition = puppet.transform.position;
            float[] vector3Serialized = {puppetPosition.x, puppetPosition.y, puppetPosition.z};
            puppetSerialized.Add(vector3Serialized);

            PuppetScript script = (PuppetScript)puppet.GetComponent(typeof(PuppetScript));
            int spriteName = script.spriteName;
            puppetSerialized.Add(spriteName);
            puppetsSerializable.Add(puppetSerialized);
        }
        SaveLoadController.control.puppets = puppetsSerializable;
    }
    public void deSerializePuppets()
    {
        foreach(ArrayList puppetSerialized in puppetsSerializable)
        {
            GameObject puppet = new GameObject();
            puppet.AddComponent<PuppetScript>();
            puppet.AddComponent<SpriteRenderer>();
            Vector3 vector = new Vector3();
            float[] serializedPosition = (float[])puppetSerialized[0];
            vector.x = serializedPosition[0];
            vector.y = serializedPosition[1];
            vector.z = serializedPosition[2];

            if ((int)puppetSerialized[1] == 1)
            {
                puppet.GetComponent<SpriteRenderer>().sprite = puppetSprite1;
            }
            puppets.Add(puppet);
        }
    }
}
