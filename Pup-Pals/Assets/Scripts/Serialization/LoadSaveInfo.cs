using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class loadSaveInfo : MonoBehaviour
{

    public List<GameObject> buttonList;
    GameObject buttn;
    private int amountOfButtons;

    // Use this for initialization
    void Start()
    {
        int i = 1;
        amountOfButtons = buttonList.Count;
        foreach (GameObject buttn in buttonList)
        {
            //GameObject button = buttonList[i];
            string playerName = SaveLoadMenu.control.getPlayerName(i);
            if (playerName != null)
            {
                buttn.GetComponentInChildren<Text>().text = playerName;
            }
            else
            {
                buttn.GetComponentInChildren<Text>().text = "Create player";
            }
            i++;
        }
    }
}