using System;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{
    public int missionType;
    public string missionName;
    public string missionDescription;
    public int missionReward;
    public int missionDuration;
    public int missionRisk;

    public Text missionNameText;
    public Text missionDescriptionText;
    public Text rewardText;
    public Text missionDurationText;
    public Text missionRiskText;

    public void Start()
    {
        generateMission();
    }

    public void generateMission()
    {
        missionType = generateMissionType();
        missionName = generateMissionName(missionType);
        missionDescription = generateMissionDescription(missionType);
        missionReward = generateReward();
        missionDuration = generateMissionDuration();
        missionRisk = generateMissionRisk();
    }

    public int generateMissionType()
    {
        int roll = UnityEngine.Random.Range(1, 100 - 1);
        if (roll <= 32)
        {
            //Food
            return 1;
        }
        if (roll >= 33 && roll <= 66)
        {
            //Wood
            return 2;
        }
        if (roll >= 67 && roll <= 99)
        {
            //Gold
            return 3;
        }
        return 0;
    }

    public string generateMissionName(int type)
    {
        switch (type)
        {
            case 1:
                return "Gathering for food";
            case 2:
                return "Gathering for wood";
            case 3:
                return "Gathering for gold";
        }
        return null;
    }

    public string generateMissionDescription(int type)
    {
        switch (type)
        {
            case 1:
                return "A villager has found a food pile outside the village! Lets go get it!";
            case 2:
                return "A villager has found a wood pile outside the village!Lets go get it";
            case 3:
                return "A villager struck a gold vein! Lets mine it!";
        }
        return null;
    }

    public int generateReward()
    {
        return UnityEngine.Random.Range(1, 40 - 1);
    }

    public int generateMissionDuration()
    {
        return UnityEngine.Random.Range(1, 8 - 1);
    }

    public int generateMissionRisk()
    {
        return UnityEngine.Random.Range(40, 90 - 1);
    }
}