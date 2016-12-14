using UnityEngine;
using System.Collections;

public abstract class Building : MonoBehaviour {

    public int slots;
    protected ArrayList puppets;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /**
     */
    public void addPuppet(GameObject puppet)
    {
        if (puppets.Count < slots)
        {
            puppets.Add(puppet);
        }
        else
        {
            Debug.Log("No slots available");
        }
    }

    public void removePuppet(GameObject puppetToRemove)
    {
        foreach (GameObject puppet in puppets)
        {
            if (puppetToRemove == puppet)
            {
                puppets.Remove(puppetToRemove);
                return;
            }
        }
    }
}
