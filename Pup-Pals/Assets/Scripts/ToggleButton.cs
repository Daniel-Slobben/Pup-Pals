using UnityEngine;
using System.Collections;

public class ToggleButton : MonoBehaviour
{

    public void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
}