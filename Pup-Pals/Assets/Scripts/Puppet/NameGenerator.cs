using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NameGenerator : MonoBehaviour
{

    string[] names = new string[] { "Marnix", "Daniel", "Achmed", "Mohammed", "Youssef", "Sagbeh" };
    string[] surnames = new string[] { "Blaauw", "Slobben", "Ackbar" };

    public string generateName()
    {
        return names[Random.Range(0, names.Length - 1)];
    }

    public string generateSurname()
    {
        return surnames[Random.Range(0, surnames.Length - 1)];
    }

}
