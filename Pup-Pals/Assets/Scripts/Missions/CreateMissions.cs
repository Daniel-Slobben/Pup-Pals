using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateMissions : MonoBehaviour
{
    public int missionType1;
    public string missionName1;
    public string missionDescription1;
    public int missionReward1;
    public int missionDuration1;
    public int missionRisk1;

    public int missionType2;
    public string missionName2;
    public string missionDescription2;
    public int missionReward2;
    public int missionDuration2;
    public int missionRisk2;

    public GameObject puppetSlot;


    public void Start()
    {
        generateMission();
    }

    public void generateMission()
    {
        missionType1 = generateMissionType();
        missionName1 = generateMissionName(missionType1);
        missionDescription1 = generateMissionDescription(missionType1);
        missionReward1 = generateReward();
        missionDuration1 = generateMissionDuration();
        missionRisk1 = generateMissionRisk();

        missionType2 = generateMissionType();
        missionName2 = generateMissionName(missionType2);
        missionDescription2 = generateMissionDescription(missionType2);
        missionReward2 = generateReward();
        missionDuration2 = generateMissionDuration();
        missionRisk2 = generateMissionRisk();
    }

    public int generateMissionType()
    {
        int roll = Random.Range(1, 100 - 1);
        if (roll <= 32)
        {
            //Food
            return 1;
        }
        if(roll >= 33 && roll <= 66)
        {
            //Wood
            return 2;
        }
        if(roll >= 67 && roll <= 99)
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
        return Random.Range(1, 40 - 1);
    }

    public int generateMissionDuration()
    {
        return Random.Range(1, 8 - 1);
    }

    public int generateMissionRisk()
    {
        return Random.Range(40, 90 - 1);
    }
}
