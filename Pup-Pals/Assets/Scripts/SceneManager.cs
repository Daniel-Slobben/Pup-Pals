/*
 * This script is used to switch to different scenes.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 08-12-2016
 */

using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{

    //Loads the play scene.
    public void loadGame()
    {
        Application.LoadLevel("game");
    }

    //Loads the menu scene.
    public void loadMenu()
    {
        Application.LoadLevel("MainMenu");
    }

    //loads the create player scene
    public void loadCreatePlayer()
    {
        Application.LoadLevel("CreatePlayer");
    }

    //Quits the game.
    public void quitGame()
    {
        Application.Quit();
    }
}
