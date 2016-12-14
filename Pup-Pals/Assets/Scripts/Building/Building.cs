using UnityEngine;
using System.Collections;

public abstract class Building : MonoBehaviour
{

    protected int slots;
    protected GameManager gameManager;
    protected ArrayList puppets;

    // Use this for initialization
    protected void Start()
    {
        Debug.Log("atleast i tried");
        gameManager = (GameManager)GameObject.FindGameObjectWithTag("GameController").GetComponent(typeof(GameManager));
    }

    // Update is called once per frame
    void Update()
    {

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
    public abstract bool cost();
}
