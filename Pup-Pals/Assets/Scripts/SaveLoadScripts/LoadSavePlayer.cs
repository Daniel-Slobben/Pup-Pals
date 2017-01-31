/*
 * This script loads the save files.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 16-12-2016
 */

using UnityEngine;
using System.Collections;

public class LoadSavePlayer : MonoBehaviour
{

    public int buttonNumber;
    public int playerIdentity;

    public void loadPlayer(int buttonNumber)
    {
        playerIdentity = buttonNumber;
        SaveLoadController.control.setPlayerIdentity(buttonNumber);
        SaveLoadController.control.Load(buttonNumber);
    }
}
