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
    public string occupation;
    public int age;

    // status effects
    public bool sanitized;
    public bool schooled;

    public int timeInSchool;

    public GameManager gameManager;

    // Use this for initialization
    void Start()
    {

        nameGenerator = new NameGenerator();
        firstName = nameGenerator.generateName();
        surname = nameGenerator.generateSurname();
        hygiene = 100;

        gameManager = (GameManager)GameObject.FindGameObjectWithTag("GameController").GetComponent(typeof(GameManager));

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void notifyPuppetEndTurn()
    {
        Debug.Log("Puppet getting notified");
        age++;
        switch(occupation)
        {
            case "farm":
                gameManager.setFood(4);
                break;
            case "workshop":
                gameManager.setWood(4);
                break;
            case "sanitation":
                // should have been handled in the building itself
                break;
            case "school":
                // should have been handled in the building itself
                break;
        }
    }

    public bool decreaseHygiene(GameObject puppet)
    {
        //This decrease amount must be set to the building where puppet is in.
        hygiene = hygiene - 5;
        if (hygiene <= 0)
        {
            Debug.Log("Hygiene is 0.. Removing..");
            return true;
        }
        return false;
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

