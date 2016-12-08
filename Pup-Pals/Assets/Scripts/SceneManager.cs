using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    //Loads the play scene.
    public void loadGame()
    {
        Application.LoadLevel("game");
    }

    //Loads the play scene.
    public void loadMenu()
    {
        Application.LoadLevel("MainMenu");
    }

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
