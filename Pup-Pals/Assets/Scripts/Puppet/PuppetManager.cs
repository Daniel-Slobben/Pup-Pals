using UnityEngine;
using System.Collections;

public class PuppetManager : MonoBehaviour {

    public string firstName;
    public string surname;
    public int health;
    public NameGenerator nameGenerator;

	// Use this for initialization
	void Start () {

        nameGenerator = new NameGenerator();
        firstName = nameGenerator.generateName();
        surname = nameGenerator.generateSurname();
        health = 100;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
