using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayAudio : MonoBehaviour
{

    public AudioSource mainMenuSound;
    public AudioSource inGameSound;
    public static PlayAudio playAudio;

    void Awake()
    {
        MakeThisTheOnlyGameManager();
    }


    void MakeThisTheOnlyGameManager()
    {
        if (playAudio == null)
        {
            DontDestroyOnLoad(gameObject);
            playAudio = this;
        }
        else
        {
            if (playAudio != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void playMenuSound()
    {
        inGameSound.Stop();
        mainMenuSound.Play();
    }

    public void playInGameSound()
    {
        mainMenuSound.Stop();
        inGameSound.Play();
    }
}
