using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class loadSaveInfo : MonoBehaviour {

    public List<GameObject> buttonList;
    GameObject buttn;
    private int amountOfButtons;

    // Use this for initialization
    void Start () {
        int i = 1;
       amountOfButtons = buttonList.Count;
        Debug.Log(amountOfButtons);
        foreach(GameObject buttn in buttonList)
        {
            Debug.Log("Trying to fetch player " + i + "'s name..");
            //GameObject button = buttonList[i];
            string playerName = SaveLoadController.control.getPlayerName(i);
            if(playerName!= null) {
                Debug.Log("Player found! Attaching " + playerName + "'s to an button..");
                buttn.GetComponentInChildren<Text>().text = playerName;
            }
            else
            {
                Debug.Log("No player found for player " + i);
                buttn.GetComponentInChildren<Text>().text = "Create player";
            }            
            i++;
        }
	}
}
