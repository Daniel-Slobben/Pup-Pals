/*
 * This script manages the toggeling of panels.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 30-11-2016
 */
using UnityEngine;
using System.Collections;

public class ToggleButton : MonoBehaviour
{
    public GameObject[] panels;

    /*
     * Sets the panel active/not active.
     * @Param panel
     */
    public void TogglePanel(GameObject panel)
    {
        panels = GameObject.FindGameObjectsWithTag("panel");

        if (panels.Length > 0)
        {
            GameObject oldPanel = panels[0];
            if (oldPanel.name == panel.name)
            {
            }
            else
            {
                oldPanel.SetActive(false);
            }
        }
        panel.SetActive(!panel.activeSelf);
    }

    public void closePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}