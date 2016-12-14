using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Build : MonoBehaviour
{

    public GameObject house;
    public GameObject farm;
    private Building buildingScript;
    private GameObject building;

    private void OnMouseDown()
    {

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 location = transform.parent.transform.parent.transform.position;

            if (gameObject.name == "HouseButton")
            { 
                building = Instantiate(house);
            }
            else if (gameObject.name == "FarmButton")
            {
                building = Instantiate(farm);
            }

            building.transform.position = location;
            buildingScript = (Building)building.GetComponent(typeof(Building));
            Invoke("cost", 0.001f);
        }
    }
    void cost()
    {
        if (buildingScript.cost())
        {
            Debug.Log("got monies");
            Destroy(transform.parent.transform.parent.gameObject);
        }
        else
        {
            Debug.Log("no monies");
            // maybe play a sound here, or make something flash. 
            Destroy(building);
        }
    }
}
