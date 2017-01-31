/*
 * The controller for the saveload controller object.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 16-12-2016
 */

using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoadController : MonoBehaviour
{

    public static SaveLoadController control;
    public string playerName;
    public int playerIdentity;
    public string currentScene;

    // Check if only one singleton excists.
    public void Start()
    {
        gameObject.GetComponent<PlayAudio>().inGameSound.Stop();
        currentScene = Application.loadedLevelName;
    }

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

    public void Update()
    {
        if (currentScene != Application.loadedLevelName && currentScene != null)
        {
            currentScene = Application.loadedLevelName;

            if (currentScene == "MainMenu" && currentScene != "CreatePlayer")
            {
                if (!gameObject.GetComponent<PlayAudio>().mainMenuSound.isPlaying)
                    gameObject.GetComponent<PlayAudio>().playMenuSound();
            }
            if (currentScene == "game")
            {
                gameObject.GetComponent<PlayAudio>().playInGameSound();
            }
        }
    }

    //Saves the game per playeridentity.
    public void Save(int playerIdentity)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + playerIdentity + ".dat");
        PlayerData data = new PlayerData();
        data.playerName = playerName;
        data.playerIdentity = playerIdentity;
        bf.Serialize(file, data);
        file.Close();
    }

    //loads the game per playeridentity.
    public void Load(int playerIdentity)
    {
        if (File.Exists(Application.persistentDataPath + "/" + playerIdentity + ".dat"))
        {
            loadGame loadGame = new loadGame();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + playerIdentity + ".dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            this.playerName = data.playerName;
            this.playerIdentity = data.playerIdentity;
            loadGame.LoadGame(playerIdentity);
        }
        else
        {
            this.playerName = "";
            Application.LoadLevel("CreatePlayer");
        }

    }

    //Sets the playerID.
    public void setPlayerIdentity(int id)
    {
        this.playerIdentity = id;
    }

    //Gets the playerID.
    public int getPlayerIdentity()
    {
        return this.playerIdentity;
    }

    //Gets the playername from the .dat file.
    public string getPlayerName(int playerId)
    {

        if (File.Exists(Application.persistentDataPath + "/" + playerId + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + playerId + ".dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            return data.playerName;
        }
        else
        {
            return null;
        }
    }

    //Sets the player name.
    public void setPlayerName(string playerName)
    {
        this.playerName = playerName;
    }
}

//Objects that are beeing serialized.
[Serializable]
class PlayerData
{
    public string playerName;
    public int playerIdentity;
}
