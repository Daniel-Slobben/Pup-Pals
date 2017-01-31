/*
 * This script sets the playername at the start of the game.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 16-12-2016
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPlayer : MonoBehaviour
{

    public InputField inputField;

    public void setPlayerName()
    {

        string playerName = inputField.text.ToString();
        if (playerName != "")
        {
            saveGame saveGame = new saveGame();

            SaveLoadController.control.setPlayerName(playerName);
            LevelSerializer.PlayerName = playerName;

            SaveLoadController.control.Save(SaveLoadController.control.getPlayerIdentity());
            saveGame.SaveGame();

            Application.LoadLevel("game");
        }
    }

}
