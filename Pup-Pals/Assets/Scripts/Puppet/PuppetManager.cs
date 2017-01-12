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
    public int age;

    // balance numbers
    public int foodCostPerTurn;
    public int farmHygieneDecrease;
    public int workshopHygieneDecrease;

    // status effects
    public bool sanitized;
    public bool schooled;
    public bool sick;

    public Sprite cursor;

    public int timeInSchool;

    public GameManager gameManager;

    // Use this for initialization
    void Start()
    {

        nameGenerator = new NameGenerator();
        firstName = nameGenerator.generateName();
        surname = nameGenerator.generateSurname();

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
        Debug.Log("Puppet getting notified");

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
        int randomNumber = Random.Range(0, 10);
        if (hygiene == 0)
        {
            sick = true;
        }
        else if (hygiene <= 10)
        {
            if (randomNumber < 8)
            {
                sick = true;
            }
        }
        else if (hygiene <= 20)
        {
            if (randomNumber < 5)
            {
                sick = true;
            }
        }
        else if (hygiene <= 30)
        {
            if (randomNumber < 3)
            {
                sick = true;
            }
        }
        else if (hygiene <= 40)
        {
            if (randomNumber < 1)
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

    void OnDestroy()
    {
        occupation.removePuppet(gameObject);
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

