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

    // Use this for initialization
    void Start()
    {

        nameGenerator = new NameGenerator();
        firstName = nameGenerator.generateName();
        surname = nameGenerator.generateSurname();
        hygiene = 100;

    }

    // Update is called once per frame
    void Update()
    {

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
}

