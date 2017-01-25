using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class missionInfo : MonoBehaviour
{

    public int missionType;
    public Text missionNameText;
    public Text missionDescriptionText;
    public Text rewardText;
    public Text missionDurationText;
    public Text missionRiskText;
    public Image missionRewardImage;
    public GameObject missionOverview1;
    public GameObject missionBusy1;
    public GameObject rewardIcon1;

    public Text warning1;
    public Text warning2;

    public int missionType2;
    public Text missionNameText2;
    public Text missionDescriptionText2;
    public Text rewardText2;
    public Text missionDurationText2;
    public Text missionRiskText2;
    public Image missionRewardImage2;
    public GameObject missionOverview2;
    public GameObject missionBusy2;
    public GameObject rewardIcon2;

    public Sprite food;
    public Sprite wood;
    public Sprite gold;

    public List<GameObject> optionalPuppets = new List<GameObject>(6);


    public void setMissionInfo()
    {
        try
        {
            GameObject missionPanel = GameObject.Find("MissionsPanel");
            if (missionPanel.activeSelf)
            {

                warning1.GetComponent<CanvasRenderer>().SetAlpha(0f);
                warning2.GetComponent<CanvasRenderer>().SetAlpha(0f);
                GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
                GameManager script = gameManager.GetComponent<GameManager>();
                CreateMissions createMissions = gameManager.GetComponent<CreateMissions>();
                //GameObject obj1 = GameObject.Find("Dropdown1");
                //GameObject obj2 = GameObject.Find("Dropdown2");
                List<GameObject> puppets = script.puppets;

                //obj1.GetComponent<Dropdown>().ClearOptions();
                //obj2.GetComponent<Dropdown>().ClearOptions();

                //Mission 1

                rewardIcon1 = GameObject.Find("rewardImage1");
                missionNameText = GameObject.Find("missionName1").GetComponent<Text>();
                missionDescriptionText = GameObject.Find("missionDescription1").GetComponent<Text>();
                rewardText = GameObject.Find("missionReward1").GetComponent<Text>();
                missionDurationText = GameObject.Find("missionDuration1").GetComponent<Text>();
                missionRiskText = GameObject.Find("missionRisk1").GetComponent<Text>();
                missionRewardImage = rewardIcon1.GetComponent<Image>();

                missionType = createMissions.missionType1;
                missionNameText.text = "" + createMissions.missionName1;
                missionDescriptionText.text = "" + createMissions.missionDescription1;
                rewardText.text = "" + createMissions.missionReward1;
                missionDurationText.text = "" + createMissions.missionDuration1.ToString() + " turns";
                missionRiskText.text = "" + createMissions.missionRisk1.ToString() + "% of succes";
                missionRewardImage.sprite = translateMissionType(missionType);

                //Mission 2
                rewardIcon2 = GameObject.Find("rewardImage2");
                missionNameText2 = GameObject.Find("missionName2").GetComponent<Text>();
                missionDescriptionText2 = GameObject.Find("missionDescription2").GetComponent<Text>();
                rewardText2 = GameObject.Find("missionReward2").GetComponent<Text>();
                missionDurationText2 = GameObject.Find("missionDuration2").GetComponent<Text>();
                missionRiskText2 = GameObject.Find("missionRisk2").GetComponent<Text>();
                missionRewardImage2 = rewardIcon2.GetComponent<Image>();

                missionType2 = createMissions.missionType2;
                missionNameText2.text = "" + createMissions.missionName2;
                missionDescriptionText2.text = "" + createMissions.missionDescription2;
                rewardText2.text = "" + createMissions.missionReward2;
                missionDurationText2.text = "" + createMissions.missionDuration2.ToString() + " turns";
                missionRiskText2.text = "" + createMissions.missionRisk2.ToString() + "% of succes";
                missionRewardImage2.sprite = translateMissionType(missionType2);

                //valuePuppetId.Clear();


            }

        }
        catch (NullReferenceException ex)
        {
        }
    }

    public void startMission1()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        GameManager script = gameManager.GetComponent<GameManager>();
        CreateMissions createMissions = gameManager.GetComponent<CreateMissions>();

        foreach (GameObject puppet in script.puppets)
        {
            PuppetManager pupje = puppet.GetComponent<PuppetManager>();
            if (!pupje.busy)
            {
                optionalPuppets.Add(puppet);
            }

        }
        int nonBusyPuppetCount = optionalPuppets.Count;
        if (nonBusyPuppetCount > 0)
        {
            int puppetSelect = UnityEngine.Random.Range(0, nonBusyPuppetCount);
            GameObject chosenOne = optionalPuppets[puppetSelect];
            PuppetManager chosenOneScript = chosenOne.GetComponent<PuppetManager>();
            chosenOneScript.busy = true;
            chosenOneScript.onMission = true;
            chosenOneScript.startMission(createMissions.missionDuration1, createMissions.missionRisk1,
            createMissions.missionReward1, createMissions.missionType1, 1, missionBusy1, missionOverview1);
            optionalPuppets.Clear();
        }
        else
        {
            warning1.GetComponent<CanvasRenderer>().SetAlpha(1f);
            warning1.GetComponentInChildren<Text>().text = "All of your puppets seem to be busy.";
            warning1.CrossFadeAlpha(0.0f, 2.0f, false);
        }
    }

    public void startMission2()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        GameManager script = gameManager.GetComponent<GameManager>();
        CreateMissions createMissions = gameManager.GetComponent<CreateMissions>();

        foreach (GameObject puppet in script.puppets)
        {
            PuppetManager pupje = puppet.GetComponent<PuppetManager>();
            if (!pupje.busy)
            {
                optionalPuppets.Add(puppet);
            }

        }

        int nonBusyPuppetCount = optionalPuppets.Count;
        if (nonBusyPuppetCount > 0)
        {
            int puppetSelect = UnityEngine.Random.Range(0, nonBusyPuppetCount);
            GameObject chosenOne = optionalPuppets[puppetSelect];
            PuppetManager chosenOneScript = chosenOne.GetComponent<PuppetManager>();
            chosenOneScript.busy = true;
            chosenOneScript.onMission = true;
            chosenOneScript.startMission(createMissions.missionDuration2, createMissions.missionRisk2,
            createMissions.missionReward2, createMissions.missionType2, 2, missionBusy2, missionOverview2);
            optionalPuppets.Clear();
        }
        else
        {
            warning2.GetComponent<CanvasRenderer>().SetAlpha(1f);
            warning2.GetComponentInChildren<Text>().text = "All of your puppets seem to be busy.";
            warning2.CrossFadeAlpha(0.0f, 2.0f, true);
        }

    }

    public Sprite translateMissionType(int type)
    {
        switch (type)
        {
            case 1:
                return food;
            case 2:
                return wood;
            case 3:
                return gold;
        }
        return null;
    }

    public bool checkAvailableMissions()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        GameManager script = gameManager.GetComponent<GameManager>();
        CreateMissions createMissions = gameManager.GetComponent<CreateMissions>();

        foreach (GameObject puppet in script.puppets)
        {
            PuppetManager pupje = puppet.GetComponent<PuppetManager>();
            if (!pupje.busy)
            {
                optionalPuppets.Add(puppet);
            }
        }
        if (optionalPuppets.Count == 0)
        {
            return true;
        }
        return false;
    }

}
