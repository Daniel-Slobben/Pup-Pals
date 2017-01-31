/*
 * This script handles the fading text at the start of the game.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 16-12-2016
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadingText : MonoBehaviour {

    Text text;

    void Start()
    {
        text = GameObject.Find("WelcomeText").GetComponent<Text>();
        text.CrossFadeAlpha(1.0f, 0.05f, false);
    }
}
