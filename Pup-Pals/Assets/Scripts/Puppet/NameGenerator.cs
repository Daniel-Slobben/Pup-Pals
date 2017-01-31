/*
 * This script generates names for the puppets.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 20-12-2016
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NameGenerator : MonoBehaviour
{

    string[] names = new string[] { "Marnix", "Daniel", "Alex", "Shilene", "Ayla", "Jeffrey" };
    string[] surnames = new string[] { "Blaauw", "Slobben", "Charron", "Vonk", "Sjoers", "van den Berg" };

    public string generateName()
    {
        return names[Random.Range(0, names.Length - 1)];
    }

    public string generateSurname()
    {
        return surnames[Random.Range(0, surnames.Length - 1)];
    }

}
