using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BuildingPlot : MonoBehaviour {

    // the icons
    public GameObject[] icons;
    public GameObject farmIcon;
    public GameObject workshopIcon;
    public GameObject schoolIcon;
    public GameObject sanitationIcon;

    // building prefabs for building the buildings
    public GameObject farm;
    public GameObject workshop;
    public GameObject school;
    public GameObject sanitation;

    // building menu
    public GameObject menu;
    private BuildMenu menuScript;

    // some bools
    private bool itemToggle = true;
    private bool menuToggle = false;

    // variables for building the actual building
    private GameObject building;
    private Building buildingScript;


    public void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (menuToggle)
            {
                menu.SetActive(false);
                menuToggle = false;
            }
            else
            {
                toggleItems();
            }
        }            
    }

    // Use this for initialization
    void Start ()
    {
        icons = new GameObject[4];
        icons[0] = farmIcon;
        icons[1] = workshopIcon;
        icons[2] = schoolIcon;
        icons[3] = sanitationIcon;

        toggleItems();
        menuScript = (BuildMenu)menu.GetComponent(typeof(BuildMenu));
        menu.SetActive(false);
    }

    void toggleItems()
    {
        if (itemToggle)
            itemToggle = false;
        else
            itemToggle = true;

        foreach (GameObject icon in icons)
        {
            icon.SetActive(itemToggle);
        }
    }

    public void showBuildMenu(string icon)
    {
        menuToggle = true;
        menu.SetActive(true);
        menuScript.info = icon;
        menuScript.setInfo();
    }

    public void build(string icon)
    {
        Vector2 location = transform.position;

        if (icon == "Farm")
            building = Instantiate(farm);
        else if (icon == "Workshop")
            building = Instantiate(workshop);
        else if (icon == "School")
            building = Instantiate(school);
        else if (icon == "Sanitation Building")
            building = Instantiate(sanitation);

        building.transform.position = location;
        buildingScript = (Building)building.GetComponent(typeof(Building));
        Invoke("cost", 0.001f);
    }
    void cost()
    {
        if (buildingScript.cost())
        {
            Debug.Log("got monies");
            Destroy(transform.gameObject);
        }
        else
        {
            Debug.Log("no monies");
            menuScript.info = "NotEnoughMoney";
            menuScript.setInfo();
            Destroy(building);
        }
    }
}
