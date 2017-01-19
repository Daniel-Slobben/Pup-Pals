using UnityEngine;
using System.Collections;

public class LaunchWashingScript : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void wash()
    {
        PuppetManager puppetScript = transform.parent.gameObject.GetComponent<PuppetPanel>().puppetSlot.GetComponent<PuppetManager>();
        puppetScript.hygiene = 100;
        puppetScript.sick = false;
        GetComponent<saveGame>().SaveGame();
        Application.LoadLevel("WashingGame");
        

    }

}
