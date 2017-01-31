/*
 * This script handles the particle effect on mouse down at the minigame.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 19-01-2017
 */


using UnityEngine;
using System.Collections;

public class startParticles : MonoBehaviour
{

    public float objectDistance = 5;
    public AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {

        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 pos = r.GetPoint(objectDistance);
        transform.position = pos;

        if (Input.GetMouseButton(0))
        {
        }
        else
        {
            gameObject.GetComponent<ParticleSystem>().Play();
        }

        if (Input.GetMouseButtonDown(0) == true)
        {
            audioSource.Play();
        }
        if (Input.GetMouseButtonUp(0) == true)
        {
            audioSource.Stop();
        }

    }
}
