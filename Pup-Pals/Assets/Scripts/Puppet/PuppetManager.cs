using UnityEngine;
using System.Collections;

public class PuppetManager : MonoBehaviour
{

    public string firstName;
    public string surname;
    public int hygiene;
    public NameGenerator nameGenerator;
    public int puppetId;

    public bool busy;
    public Building occupation;
    public Building working;
    public int age;

    // balance numbers
    public int foodCostPerTurn;
    public int farmHygieneDecrease;
    public int workshopHygieneDecrease;

    // status effects
    public bool sanitized;
    public bool schooled;
    public bool sick;
    public bool muddy;

    public Sprite cursor;

    public int timeInSchool;

    public GameManager gameManager;

    public bool onMission;
    public int missionDuration;
    public int missionReward;
    public int missionRisk;
    public int missionType;
    public GameObject busyMissionPanel;
    public GameObject missionPanel;

    public bool notFirstPlay;

    // Use this for initialization
    void Start()
    {
        if (!notFirstPlay)
        {
            nameGenerator = new NameGenerator();
            firstName = nameGenerator.generateName();
            surname = nameGenerator.generateSurname();
            notFirstPlay = true;
        }
        onMission = false;

        GetComponent<SpriteRenderer>().sprite = cursor;
        gameManager = (GameManager)GameObject.FindGameObjectWithTag("GameController").GetComponent(typeof(GameManager));

    }

    // Update is called once per frame
    void Update()
    {

    }

    /**
     * the boolean is for checking if the puppet died
     */
    public bool notifyPuppetEndTurn()
    {

        if (makePuppetsDie())
        {
            return true;
        }

        age++;

        eat();

        applySickness();

        int hygieneToDecrease = checkOccupation(); // checkOccupation makes the puppets work.

        decreaseHygiene(hygieneToDecrease);
        return false; // return true if puppet died, if puppet died return feedback to the player
    }

    private bool makePuppetsDie()
    {
        if (sick)
        {
            int randomNumber = Random.Range(0, 4);
            if (randomNumber < 1)
            {
                return true;
                // puppet died, we should probably notify the user here;
            }
            if (randomNumber > 3)
            {
                sick = false;
                // puppet is cured, we should probably notify the user here;
            }
        }
        return false;
    }

    private void eat()
    {
        gameManager.setFood(foodCostPerTurn);
    }

    private void applySickness()
    {
        int randomNumber = Random.Range(1, 10);
        if (hygiene == 0)
        {
            sick = true;
        }
        else if (hygiene <= 10)
        {
            if (randomNumber <= 8)
            {
                sick = true;
            }
        }
        else if (hygiene <= 20)
        {
            if (randomNumber <= 5)
            {
                sick = true;
            }
        }
        else if (hygiene <= 30)
        {
            if (randomNumber <= 3)
            {
                sick = true;
            }
        }
        else if (hygiene <= 40)
        {
            if (randomNumber == 1)
            {
                sick = true;
            }
        }
        if (sick)
        {
            // we should notify the user here. 
        }
    }

    private void decreaseHygiene(int decrease)
    {
        //This decrease amount must be set to the building where puppet is in.
        // check if the puppet is sanitized.
        if (sanitized)
        {
            decrease = decrease / 2;
        }
        hygiene -= decrease;
    }

    private int checkOccupation()
    {
        int hygieneToDecrease = 10;
        if (occupation != null)
        {
            switch (occupation.buildingName)
            {
                case "farm":
                    gameManager.setFood(4);
                    hygieneToDecrease = farmHygieneDecrease;
                    break;
                case "workshop":
                    gameManager.setWood(4);
                    hygieneToDecrease = workshopHygieneDecrease;
                    break;
                case "sanitation":
                    // should have been handled in the building itself
                    break;
                case "school":
                    // should have been handled in the building itself
                    break;
            }
            // check if the puppet isnt in a sanitation building
            if (occupation.buildingName != "sanitation")
            {
                sanitized = false;
            }
        }

        return hygieneToDecrease;
    }

    public void startMission(int duration, int risk, int reward, int type, int missionNumber, GameObject busyPanel, GameObject missionView)
    {
        if (onMission == true)
        {

            missionPanel = missionView;
            busyMissionPanel = busyPanel;
            missionPanel.SetActive(false);

            missionType = type;
            missionReward = reward;
            missionRisk = risk;
            missionDuration = duration;

            busyPanel.SetActive(true);
        }
    }

    public void checkMissionStatus()
    {
        missionDuration--;
        if (missionDuration == 0)
        {
            busyMissionPanel.SetActive(false);
            missionPanel.SetActive(true);
            onMission = false;
            busy = false;
            int rng = Random.Range(40, 100 - 1);
            if (rng <= missionRisk)
            {
                switch (missionType)
                {
                    case 1:
                        gameManager.setFood(missionReward);
                        gameManager.showEventPanel("A mission succeeded! You have earned " + missionReward + " food!");
                        break;
                    case 2:
                        gameManager.setWood(missionReward);
                        gameManager.showEventPanel("A mission succeeded! You have earned " + missionReward + " wood!");
                        break;
                    case 3:
                        gameManager.setMoney(missionReward);
                        gameManager.showEventPanel("A mission succeeded! You have earned " + missionReward + " gold!");
                        break;
                }

            }
            else
            {
                gameManager.showEventPanel("A mission failed");
            }

        }
    }

    void OnDestroy()
    {
        removeAllActivities();
    }

    public void removeAllActivities()
    {
        Debug.Log("removed pupppet activiuty");
        busy = false;
        if (occupation != null)
        {
            occupation.removePuppet(gameObject);
            occupation = null;
            working = null;
        }
        else if (working != null)
        {
            working.removePuppet(gameObject);
            occupation = null;
            working = null;
        }
    }

    public int getAge()
    {
        return age;
    }
    public void sanitize()
    {
        sanitized = true;
    }
    public void school()
    {
        schooled = true;
    }
}

