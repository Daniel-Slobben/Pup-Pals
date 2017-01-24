using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    public bool isOver;

    public GameObject extraInfo;
    public GameObject textInfo;
    
    public Text textObject;

    public string buildingName;

    // Use this for initialization
    protected void Start()
    {
        textObject = textInfo.GetComponent<Text>();
        gameManager = (GameManager)GameObject.FindGameObjectWithTag("GameController").GetComponent(typeof(GameManager));

        if (!firstTry)
        {
            puppets = new ArrayList(slots);
            firstTry = true;
            currentSprite = construction;
            gameManager.addBuilding(this);
        }
        GetComponent<SpriteRenderer>().sprite = currentSprite;

        
        updateText();
        gameObject.transform.parent = GameObject.FindGameObjectWithTag("BuildingCanvas").transform;
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
        updateText();
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
                updateText();
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
        updateText();
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
            updateText();
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

    private void OnMouseEnter()
    {
        isOver = true;
        extraInfo.SetActive(true);
    }
    
    private void OnMouseExit()
    {
        isOver = false;
        extraInfo.SetActive(false);
    }

    /**
     */
    private void updateText()
    {
        if (!build)
        {
            string text = "There are currently " + puppets.Count + System.Environment.NewLine + "puppets inside." + 
                System.Environment.NewLine + System.Environment.NewLine + "This building takes " +timeToBuild+ System.Environment.NewLine+"turns to build."+
                System.Environment.NewLine+ System.Environment.NewLine+"This building requires" + System.Environment.NewLine+slotsToBuild+" puppets to build.";
            if (textObject != null)
            {
                textObject.text = "" + text;
            }
        }
        if (build)
        {
            string text = "There are currently " + puppets.Count + System.Environment.NewLine + "puppets inside." +
                System.Environment.NewLine + System.Environment.NewLine;
            if (buildingName == "farm")
            {
                text = text + "The farm is making" + System.Environment.NewLine + 4 * puppets.Count + " food per turn.";
            }
            else if (buildingName == "workshop")
            {
                text = text + "The workshop is making" + System.Environment.NewLine + 4 * puppets.Count + " wood per turn.";
            }
            else if (buildingName == "sanitation")
            {
                text = text + "This building heals the 2"+ System.Environment.NewLine+"oldest puppets in the building.";
            }
            else if (buildingName == "school")
            {
                text = text + "The puppets in this building" + System.Environment.NewLine + "get educated.";
            }
            if (textObject != null)
            {
                textObject.text = "" + text;
            }
        }
    }
}
