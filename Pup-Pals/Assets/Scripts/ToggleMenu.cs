/*
 * This script handles menu popup from escape key.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 14-12-2016
 */

using UnityEngine;
using System.Collections;

public class ToggleMenu : MonoBehaviour
{

    public GameObject menu; // Assign in inspector
    private bool isShowing;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
    }
}