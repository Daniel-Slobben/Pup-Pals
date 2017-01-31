/*
 * This script handles bottom puppet panel.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 20-12-2016
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class PuppetPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject puppetSlot;
    public int slotId;

    public Sprite puppetSprite;
    public Sprite emptySprite;

    public Sprite farmSprite;
    public Sprite workshopSprite;
    public Sprite schoolSprite;
    public Sprite sanitationSprite;

    public Sprite healthyPuppet;
    public Sprite unhealthyPuppet;

    public Sprite building;
    public Sprite onMission;

    public GameObject occupationIcon;
    public GameObject healthIcon;
    public GameObject overImageOccupation;
    public GameObject launchPuppetWashing;
    public GameObject HoverPanel;
    public GameObject Text;

    public bool isOver = false;

    // Update is called once per frame
    void Update()
    {
        updateSlot();
    }

    public void ifClicked()
    {
        Cursor.SetCursor(puppetSlot.GetComponent<SpriteRenderer>().sprite.texture, Vector2.zero, CursorMode.Auto);
        GameManager.PuppetTransport = puppetSlot;
        Debug.Log("cursor should be set");
    }

    private void updateSlot()
    {

        if (puppetSlot != null)
        {
            launchPuppetWashing.SetActive(true);
            healthIcon.SetActive(true);
            PuppetManager puppetScript = puppetSlot.GetComponent<PuppetManager>();
            GetComponent<Image>().sprite = puppetSprite;

            if (isOver)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if (puppetSlot != null)
                    {
                        puppetScript.removeAllActivities();
                    }
                }
            }

            if (puppetScript.occupation != null)
            {
                occupationIcon.SetActive(true);
                overImageOccupation.SetActive(false);
                switch (puppetScript.occupation.buildingName)
                {
                    case "farm":
                        occupationIcon.GetComponent<Image>().sprite = farmSprite;
                        break;
                    case "workshop":
                        occupationIcon.GetComponent<Image>().sprite = workshopSprite;
                        break;
                    case "sanitation":
                        occupationIcon.GetComponent<Image>().sprite = sanitationSprite;
                        break;
                    case "school":
                        occupationIcon.GetComponent<Image>().sprite = schoolSprite;
                        break;
                }
            }
            else if (puppetScript.busy)
            {
                if (puppetScript.working == null)
                {
                    overImageOccupation.SetActive(true);
                    launchPuppetWashing.SetActive(false);
                    overImageOccupation.GetComponent<Image>().sprite = onMission;
                }
                else
                {
                    overImageOccupation.SetActive(true);
                    launchPuppetWashing.SetActive(false);
                    overImageOccupation.GetComponent<Image>().sprite = building;
                }
            }
            else
            {
                occupationIcon.SetActive(false);
                overImageOccupation.SetActive(false);
            }
            if (puppetScript.sick)
            {
                healthIcon.GetComponent<Image>().sprite = unhealthyPuppet;
            }
            else
            {
                healthIcon.GetComponent<Image>().sprite = healthyPuppet;
            }
        }
        else
        {
            GetComponent<Image>().sprite = emptySprite;
            healthIcon.SetActive(false);
            occupationIcon.SetActive(false);
            overImageOccupation.SetActive(false);
            launchPuppetWashing.SetActive(false);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
        if (puppetSlot != null)
        {
            updateText();
            HoverPanel.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
        if (puppetSlot != null)
        {
            updateText();
        }
        HoverPanel.SetActive(false);
    }

    private void updateText()
    {
        Text textObject = Text.GetComponent<Text>();
        PuppetManager script = puppetSlot.GetComponent<PuppetManager>();
        textObject.text = "Name: " + script.firstName + " " + script.surname + System.Environment.NewLine + "hygiene: " + script.hygiene;
    }
}
