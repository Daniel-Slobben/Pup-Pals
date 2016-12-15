using UnityEngine;
using System.Collections;

public class LoadSavePlayer : MonoBehaviour
{
    public int buttonNumber;
    public int playerIdentity;

    public void loadPlayer(int buttonNumber)
    {
        playerIdentity = buttonNumber;
        Debug.Log(playerIdentity);
        SaveLoadMenu.control.buttonNumber = buttonNumber;
    }

    public void savePlayer()
    {
        Debug.Log("saved!");
        SaveLoadMenu.control.SaveGame(SaveLoadMenu.control.getPlayerIdentity());
    }
}