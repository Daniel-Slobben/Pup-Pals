using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPlayer : MonoBehaviour {

   public InputField inputField;

    public void setPlayerName()
    {

        string playerName = inputField.text.ToString();
        if(playerName != "")
        {
            SaveLoadController.control.setPlayerName(playerName);
            Application.LoadLevel("game");
        }
    }

}
