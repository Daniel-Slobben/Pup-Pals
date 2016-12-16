using UnityEngine;
using System.Collections;

public class LoadSavePlayer : MonoBehaviour {

    public int buttonNumber;
    public int playerIdentity;

    public void loadPlayer(int buttonNumber)
    {
        playerIdentity = buttonNumber;
        Debug.Log(playerIdentity);
        SaveLoadController.control.setPlayerIdentity(buttonNumber);
        SaveLoadController.control.Load(buttonNumber);
    }

    public void savePlayer()
    {
        GameManager gameManager = (GameManager)GameObject.FindGameObjectWithTag("GameController").GetComponent(typeof(GameManager));
        gameManager.serializePuppets();
        Debug.Log("saved!");
        SaveLoadController.control.Save(SaveLoadController.control.getPlayerIdentity());
    }
}
