using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadController : MonoBehaviour
{
    public GameObject puppetPrefab;

    public static SaveLoadController control;
    public string playerName;
    public int food;
    public int buldingMaterials;
    public int money;
    public int turnNumber;
    public int playerIdentity;
    public GameObject gameObjectToSave;
    public string someGameObject_id;

    // Check if only one singleton excists.
    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

    }

    public void Save(int playerIdentity)
    {
        OnSerialize();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo" + playerIdentity + ".dat");
        PlayerData data = new PlayerData();
        data.playerName = playerName;
        data.food = food;
        data.buldingMaterials = buldingMaterials;
        data.money = money;
        data.turnNumber = turnNumber;
        data.playerIdentity = playerIdentity;
        data.someGameObject_id = someGameObject_id;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load(int playerIdentity)
    //Om aan te roepen: SaveLoadController.control.Load();
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo" + playerIdentity + ".dat"))
        {            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo" + playerIdentity + ".dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            
            this.playerName = data.playerName;
            this.food = data.food;
            this.buldingMaterials = data.buldingMaterials;
            this.money = data.money;
            this.turnNumber = data.turnNumber;
            this.playerIdentity = playerIdentity;
            this.someGameObject_id = data.someGameObject_id;
            OnDeserialize();

            Application.LoadLevel("game");
        }
        else
        {
            this.playerName = "";
            this.food = 100;
            this.buldingMaterials = 100;
            this.money = 100;
            this.turnNumber = 0;
            this.playerIdentity = playerIdentity;
            this.gameObjectToSave = Instantiate(puppetPrefab);
            Application.LoadLevel("CreatePlayer");
        }
    }

    public void setPlayerIdentity(int id)
    {
        this.playerIdentity = id;
    }

    public int getPlayerIdentity()
    {
        return this.playerIdentity;
    }

    public string getPlayerName(int playerId)
    {

        if (File.Exists(Application.persistentDataPath + "/playerInfo" + playerId + ".dat"))
        {
            OnDeserialize();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo" + playerId + ".dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            return data.playerName;
        }
        else
        {
            return null;
        }
    }

    public void setPlayerName(string playerName)
    {
        Debug.Log("Set player name!");
        this.playerName = playerName;
    }

    public void OnSerialize()
    {
        //This is an example of a OnSerialize method, called before a gameobject is packed into serializable form.
        //In this case, the GameObject variable "someGameObject" and those in the testClass and testclass Array instances of TestClass should be reconstructed after loading.
        //Since GameObject (and Transform) references assigned during runtime can't be serialized directly, 
        //we keep a seperate string variable for each GO variable that holds the ID of the GO instead.
        //This allows us to just save the ID instead.

        //This example is one way of dealing with GameObject (and Transform) references. If a lot of those occur in your project,
        //it might be more efficient to go directly into the static SaveLoad.PackComponent method. and doing it there.

        if (gameObjectToSave != null && gameObjectToSave.GetComponent<ObjectIdentifier>())
        {
            someGameObject_id = gameObject.GetComponent<ObjectIdentifier>().id;
        }
        else
        {
            someGameObject_id = null;
        }
    }

    public void OnDeserialize()
    {

        //Since we saved the ID of the GameObject references, we can now use those to recreate the references. 
        //We just iterate through all the ObjectIdentifier component occurences in the scene, compare their id value to our saved and loaded someGameObject id (etc.) value,
        //and assign the component's GameObject if it matches.
        //Note that the "break" command is important, both because it elimitates unneccessary iterations, 
        //and because continuing after having found a match might for some reason find another, wrong match that makes a null reference.

        ObjectIdentifier[] objectsIdentifiers = FindObjectsOfType(typeof(ObjectIdentifier)) as ObjectIdentifier[];

        if (string.IsNullOrEmpty(someGameObject_id) == false)
        {
            foreach (ObjectIdentifier objectIdentifier in objectsIdentifiers)
            {

                if (string.IsNullOrEmpty(objectIdentifier.id) == false)
                {
                    if (objectIdentifier.id == someGameObject_id)
                    {
                        gameObjectToSave = objectIdentifier.gameObject;
                        break;
                    }
                }
            }
        }        
    }

}
[Serializable]
public class PlayerData
{
    public string playerName;
    public int food;
    public int buldingMaterials;
    public int money;
    public int turnNumber;
    public int playerIdentity;
    public string someGameObject_id;
}

