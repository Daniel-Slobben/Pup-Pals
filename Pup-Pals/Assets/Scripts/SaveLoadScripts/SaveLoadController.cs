using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class SaveLoadController : MonoBehaviour
{

    public static SaveLoadController control;
    public string playerName;
    public int food;
    public int buldingMaterials;
    public int money;
    public int turnNumber;
    public int playerIdentity;
    public ArrayList puppetArray;

    public bool emptySlot;

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
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo" + playerIdentity + ".dat");
        PlayerData data = new PlayerData();
        data.playerName = playerName;
        data.food = food;
        data.buldingMaterials = buldingMaterials;
        data.money = money;
        data.turnNumber = turnNumber;
        data.puppetArray = puppetArray;
        data.playerIdentity = playerIdentity;

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
            this.puppetArray = data.puppetArray;
            this.playerIdentity = playerIdentity;
            Application.LoadLevel("game");
        }
        else
        {
            this.playerName = "";
            this.food = 100;
            this.buldingMaterials = 100;
            this.money = 100;
            this.turnNumber = 0;
            this.puppetArray = new ArrayList(6);
            this.playerIdentity = playerIdentity;
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

}

[Serializable]
class PlayerData
{
    public string playerName;
    public int food;
    public int buldingMaterials;
    public int money;
    public int turnNumber;
    public ArrayList puppetArray;
    public int playerIdentity;    
}
