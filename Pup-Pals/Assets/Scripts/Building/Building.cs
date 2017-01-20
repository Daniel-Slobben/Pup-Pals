using UnityEngine;
using System.Collections;

public abstract class Building : MonoBehaviour
{

    public int slots;
    protected GameManager gameManager;
    public ArrayList puppets;
    public Sprite construction;
    public Sprite building;
    public Sprite currentSprite;
    public bool build;
    public int slotsToBuild;
    public int buildProgress;
    public int timeToBuild;

    public bool firstTry;

    public string buildingName;

    // Use this for initialization
    protected void Start()
    {
        gameManager = (GameManager)GameObject.FindGameObjectWithTag("GameController").GetComponent(typeof(GameManager));
        
        if (!firstTry)
        {
            puppets = new ArrayList(slots);
            firstTry = true;
            currentSprite = construction;
            gameManager.addBuilding(this);
        }
        GetComponent<SpriteRenderer>().sprite = currentSprite;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /**
     */
    public void addPuppet(GameObject puppet)
    {
        PuppetManager puppetScript = (PuppetManager)puppet.GetComponent(typeof(PuppetManager));
        if (puppets.Count < slots && !puppetScript.busy)
        {
            puppetScript.busy = true;
            puppetScript.working = this;
            if (build)
            {
                puppetScript.occupation = this;
            }
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
                PuppetManager puppetScript = (PuppetManager)puppetToRemove.GetComponent(typeof(PuppetManager));
                puppetScript.busy = false;
                puppets.Remove(puppetToRemove);
                return;
            }
        }
    }

    /**
     * Change sprite to done
     */
    protected void changeAnimationTobuild()
    {
        GetComponent<SpriteRenderer>().sprite = building;
        currentSprite = building;
    }
    public abstract bool cost();
    public void nextTurn()
    {
        if (!build && puppets.Count >= slotsToBuild)
        {
            buildProgress += 1;
            if (buildProgress >= timeToBuild)
            {
                build = true;
                changeAnimationTobuild();
                setOccupation();
            }
        }
        if (build)
        {
            specialBuildingAction();
        }
    }

    private void setOccupation()
    {
        foreach (GameObject puppet in puppets)
        {
            PuppetManager puppetScript = puppet.GetComponent<PuppetManager>();
            puppetScript.occupation = this;
        }
    }

    protected abstract void specialBuildingAction();

    private void OnMouseUp()
    {
        if (GameManager.PuppetTransport != null)
        {
            addPuppet(GameManager.PuppetTransport);
            GameManager.PuppetTransport = null;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
