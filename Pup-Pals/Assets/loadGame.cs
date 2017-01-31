/*
 * This script laods serialized save files.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 16-12-2016
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class loadGame : MonoBehaviour
{

    private Lookup<string, List<LevelSerializer.SaveEntry>> savedGames;
    public string saveData;
    public void LoadGame(int playerIdentity)
    {
        saveData = System.IO.File.ReadAllText(Application.persistentDataPath + "/" + playerIdentity + ".txt");
        LevelSerializer.LoadSavedLevel(saveData);
    }
}
