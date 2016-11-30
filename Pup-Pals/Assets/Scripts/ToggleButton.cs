/*
 * This script manages the toggeling of panels.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 30-11-2016
 */
using UnityEngine;
using System.Collections;

public class ToggleButton : MonoBehaviour
{
    /*
     * Sets the panel active/not active.
     * @Param panel
     */
    public void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
}