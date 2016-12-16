using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadingText : MonoBehaviour {

    Text text;

    void Start()
    {
        text = GameObject.Find("WelcomeText").GetComponent<Text>();
        text.CrossFadeAlpha(1.0f, 0.05f, false);
    }
}
