using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class loadSaveInfo : MonoBehaviour {
    public static loadSaveInfo safeInfo;
    public List<GameObject> buttonList;
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
