using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class loadGame : MonoBehaviour {

    private Lookup<string, List<LevelSerializer.SaveEntry>> savedGames;
    public string saveData;
    public void LoadGame(int playerIdentity)
    {
        Debug.Log("Loading " + SaveLoadController.control.playerName + "'s savegame file.");
        saveData = System.IO.File.ReadAllText(Application.persistentDataPath + "/" + playerIdentity + ".txt");
        LevelSerializer.LoadSavedLevel(saveData);
    }
}
