/*
 * This script checks for excisting save files.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 12-01-2017
 */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class loadSaveInfo : MonoBehaviour {
    public static loadSaveInfo safeInfo;
    public List<GameObject> buttonList;         //List of all the button objects from the player selection scene.
    GameObject buttn;
    private int amountOfButtons;

    void Awake()
    {
        if (safeInfo == null)
        {
            safeInfo = this;
        }
        else if (safeInfo != this)
        {
            Destroy(this);
        }

    }

    // Use this for initialization
    void Start () {
        getInfo();
    }

    public void getInfo()
    {
        int i = 1;
        amountOfButtons = buttonList.Count;
        Debug.Log(amountOfButtons);
        foreach (GameObject buttn in buttonList)
        {
            string playerName = SaveLoadController.control.getPlayerName(i);
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
