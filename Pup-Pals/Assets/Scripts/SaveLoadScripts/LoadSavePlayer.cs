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
