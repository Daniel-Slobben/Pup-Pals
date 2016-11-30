using UnityEngine;
using System.Collections;

public class Build : MonoBehaviour {

    public GameObject house;
    public GameObject farm;

    private void OnMouseDown()
    {
        if (gameObject.name == "HouseButton")
        {
            Vector2 location = destroyParent();
            GameObject building = Instantiate(house);
            building.transform.position = location;
        }
        else if (gameObject.name == "FarmButton")
        {
            Vector2 location = destroyParent();
            GameObject building = Instantiate(farm);
            building.transform.position = location;
        }
    }

    private Vector2 destroyParent()
    {
        Vector2 locationPoint = transform.parent.transform.parent.transform.position;
        Destroy(transform.parent.transform.parent.gameObject);
        return locationPoint;
    } 
}
