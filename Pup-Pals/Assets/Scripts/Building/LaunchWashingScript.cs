using UnityEngine;
using System.Collections;

public class LaunchWashingScript : MonoBehaviour {

    public void wash()
    {
        PuppetManager puppetScript = transform.parent.gameObject.GetComponent<PuppetPanel>().puppetSlot.GetComponent<PuppetManager>();
        puppetScript.hygiene = 100;
        puppetScript.sick = false;
        GetComponent<saveGame>().SaveGame();
        Application.LoadLevel("WashingGame");
    }

}
