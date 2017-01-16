using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class missionInfo : MonoBehaviour
{

    public Text missionNameText;
    public Text missionDescriptionText;
    public Text rewardText;
    public List<int> valuePuppetId = new List<int>(6);

    public void setMissionInfo()
    {

        try
        {
            GameObject missionPanel = GameObject.Find("MissionsPanel");
            if (missionPanel.activeSelf)
            {


                GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
                GameManager script = gameManager.GetComponent<GameManager>();
                CreateMissions createMissions = gameManager.GetComponent<CreateMissions>();
                GameObject obj = GameObject.Find("Dropdown");
                List<GameObject> puppets = script.puppets;

                obj.GetComponent<Dropdown>().ClearOptions();


                missionNameText = GameObject.Find("missionName").GetComponent<Text>();
                missionDescriptionText = GameObject.Find("missionDescription").GetComponent<Text>();
                rewardText = GameObject.Find("missionReward").GetComponent<Text>();

                missionNameText.text = "" + createMissions.missionName;
                missionDescriptionText.text = "" + createMissions.missionDescription;
                rewardText.text = "" + createMissions.missionReward;

                valuePuppetId.Clear();

                foreach (GameObject puppet in puppets)
                {
                    PuppetManager pupje = puppet.GetComponent<PuppetManager>();
                    if (!pupje.busy)
                    {
                        obj.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() { text = pupje.firstName });
                        valuePuppetId.Add(pupje.puppetId);
                    }

                }
            }

        }
        catch (NullReferenceException ex)
        {
        }
    }

    public void startMission()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        GameManager script = gameManager.GetComponent<GameManager>();
        GameObject obj = GameObject.Find("Dropdown");
        int selectedValue = obj.GetComponent<Dropdown>().value;
        Debug.Log("selectedValue = " + selectedValue);
        int puppetId = valuePuppetId[selectedValue];
        Debug.Log("puppetId = " + puppetId);
        GameObject puppet = script.puppets[puppetId];
        PuppetManager puppetScript = puppet.GetComponent<PuppetManager>();
        puppetScript.busy = true;
        Debug.Log("puppet " + puppetId + " is send on a mission..");

        //puppet ophalen van dropdown
        //id van puppet in dropdown halen
        //puppet op busy zetten
        //mission starten..
    }

}
