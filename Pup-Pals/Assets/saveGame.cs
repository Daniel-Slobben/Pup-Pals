using UnityEngine;


public class saveGame : MonoBehaviour
{

    public string saveData;
    public int identity;

    public void SaveGame()
    {
        Debug.Log("Saving " + SaveLoadController.control.playerName + "'s game.");
        identity = SaveLoadController.control.getPlayerIdentity();
        Debug.Log("Identity in save game: " + identity);
        LevelSerializer.PlayerName = identity.ToString();
        saveData = LevelSerializer.SerializeLevel();
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + identity + ".txt", saveData);
        SaveLoadController.control.Save(identity);
    }

}
