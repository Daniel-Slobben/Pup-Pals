﻿using UnityEngine;
using System.Collections;

public class PuppetPanel : MonoBehaviour {

    public GameObject puppetSlot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ifClicked()
    {
        Cursor.SetCursor(puppetSlot.GetComponent<SpriteRenderer>().sprite.texture, Vector2.zero, CursorMode.Auto);
        GameManager.PuppetTransport = puppetSlot;
        Debug.Log("cursor should be set");
    }
}
