/*
 * This script creates serialized save files.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 16-12-2016
 */

using UnityEngine;


public class saveGame : MonoBehaviour
{

    public string saveData;
    public int identity;

    public void SaveGame()
    {
        identity = SaveLoadController.control.getPlayerIdentity();
        LevelSerializer.PlayerName = identity.ToString();
        saveData = LevelSerializer.SerializeLevel();
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + identity + ".txt", saveData);
        SaveLoadController.control.Save(identity);
    }

}
