/*
 * This script handles the endgame state of PupPals.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 19-01-2017
 */

using UnityEngine;
using System.Collections;

public class gameOver : MonoBehaviour
{

    public void backToMainMenu()
    {
        Application.LoadLevel("MainMenu");
    }
}
