using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateMissions : MonoBehaviour
{
    public ArrayList missionDetails;
    public string missionName;
    public string missionDescription;
    public GameObject puppetSlot;
    public int missionReward;
    public int succesChance;

    public void Start()
    {
        generateMission();
    }

    public void generateMission()
    {
        Debug.Log("Creating mission..");
        missionName = generateMissionName();
        missionDescription = generateMissionDescription();
        missionReward = generateReward();
    }

    public string generateMissionName()
    {
        string[] names = new string[] { "Aid another village", "Gathering for food", "Gathering for wood", "Gathering for gold" };
        return names[Random.Range(0, names.Length - 1)];
    }

    public string generateMissionDescription()
    {
        string[] descriptions = new string[] {
            "Go and help another village, this will not only help them but it will also help your own village!",
            "A villager has found a food pile outside the village! Lets go get it!",
            "A villager has found a wood pile outside the village! Lets go get it",
            "I villager struck a gold vein! Lets mine it!" };
        return descriptions[Random.Range(0, descriptions.Length - 1)];
    }
    public int generateReward()
    {
        return Random.Range(0, 100 - 1);
    }
}
