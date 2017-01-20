using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;

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
        currentScene = EditorApplication.currentScene;
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
        if(currentScene != EditorApplication.currentScene && currentScene != null)
        {
            currentScene = EditorApplication.currentScene;

            if(currentScene == "Assets/Scenes/MainMenu.unity" && currentScene != "Assets/Scenes/CreatePlayer.unity")
            {
                if(!gameObject.GetComponent<PlayAudio>().mainMenuSound.isPlaying)
                gameObject.GetComponent<PlayAudio>().playMenuSound();
            }
            if (currentScene == "Assets/Scenes/game.unity")
            {
                gameObject.GetComponent<PlayAudio>().playInGameSound();
            }
        }
    }

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

    public void Load(int playerIdentity)
    //Om aan te roepen: SaveLoadController.control.Load();
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
    public int playerIdentity;
}
