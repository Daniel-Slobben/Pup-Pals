using UnityEngine;
using System.Collections;

public class gameOver : MonoBehaviour {

    public void backToMainMenu()
    {
        Application.LoadLevel("MainMenu");
    }
}
