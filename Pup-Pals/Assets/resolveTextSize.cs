/*
 * This script resolves long string problems.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 18-01-2017
 */


using UnityEngine;
using System.Collections;

public class resolveTextSize : MonoBehaviour
{

    public string input;
    public int lineLength;

    public void Start()
    {

        TextMesh text = gameObject.GetComponentInChildren<TextMesh>();
        text.text = ResolveTextSize(input, lineLength);
    }

    private string ResolveTextSize(string input, int lineLength)
    {
       
        string[] words = input.Split(" "[0]);
        string result = "";
        string line = "";
        
        foreach (string s in words)
        {
            string temp = line + " " + s;
            if (temp.Length > lineLength)
            {
                result += line + "\n";
                line = s;
            }
            else
            {
                line = temp;
            }
        }
             
        result += line;
        return result.Substring(1, result.Length - 1);
    }
}
