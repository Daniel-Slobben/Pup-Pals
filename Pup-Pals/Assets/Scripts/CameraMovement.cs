/*
 * This script manages the ingame camera scrolling and movement.
 * @author Marnix Blaauw & Daniel Slobben
 * @datecreated 30-11-2016
 */

using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    
    public float movementZone = 30;         //Border variable that interacts with the mouse.
    public float movementSpeed = 0.1f;      //Movement speed of the camera.

    public float mapX;                      //Map/background width.
    public float mapY;                      //Map/background height.

    private float zoom = 5;                 //Standard zoom variable.
    private float zoomSpeed = 0.2f;         //Zoom speed variable.
    private float zoomMax = 7;             //Max zoom variable.
    private float zoomMin = 3;              //Min zoom variable.
    
    private Vector3 desiredPosition;

    void Start()
    {
        desiredPosition = transform.position;
    }

    /*
     * Gets called every frame
     * checks for arrow keys movement and mouse movement
     * adjusts the camera based on the input
     * Checks if the camera does not go out of bounds 
     */
	void Update ()
    {
        float x = 0, y =0, z = 0;
        float speed = movementSpeed * Time.deltaTime;

        Vector3 move;
        
        updateZoom();
        
        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        float leftBound = horzExtent - mapX / 2;
        float rightBound = mapX / 2 - horzExtent;
        float bottomBound = vertExtent - mapY / 2;
        float topBound = mapY / 2 - vertExtent;

        // WASD/Arrow keys movement
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");

        if (horizontalAxis != 0|| verticalAxis != 0)
        {
            // horizontal movement
            if (horizontalAxis < 0)
                x -= speed;
            if (horizontalAxis > 0)
                x += speed;
            // vertical
            if (verticalAxis < 0)
                y -= speed;
            if (verticalAxis > 0)
                y += speed;

            if (verticalAxis == 0)
                y = 0;
            if (horizontalAxis == 0)
                x = 0;

            move = new Vector3(x, y, z) + transform.position;
        }
        // Mouse movement
        else
        {            
            if (Input.mousePosition.x < movementZone)
            {
                x -= speed;
            }
            else if (Input.mousePosition.x > Screen.width - movementZone)
            {
                x += speed;
            }

            if (Input.mousePosition.y < movementZone)
            {
                y -= speed;
            }
            else if (Input.mousePosition.y > Screen.height - movementZone)
            {
                y += speed;
            }
            move = new Vector3(x, y, z) + desiredPosition;
        }

        float camX = Mathf.Clamp(move.x, leftBound, rightBound);
        float camY = Mathf.Clamp(move.y, bottomBound, topBound);

        if (camX == leftBound)
            camX += 0.01f;
        if (camX == rightBound)
            camX -= 0.01f;
        if (camY == bottomBound)
            camY += 0.01f;
        if (camY == topBound)
            camY -= 0.01f;

        var newCameraVector = new Vector3(camX, camY, Camera.main.transform.position.z);

        if (Camera.main.transform.position.x != newCameraVector.x || Camera.main.transform.position.y != newCameraVector.y)
        {
            desiredPosition = newCameraVector;
        }
        Camera.main.transform.position = newCameraVector;
    }

    /**
     * Manages the scrolling
     */
    private void updateZoom()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            float tempZoom = zoom;
            if (scroll > 0f)
            {
                // scroll up
                tempZoom -= zoomSpeed;
            }
            else if (scroll < 0f)
            {
                // scroll down
                tempZoom += zoomSpeed;
            }
            if (tempZoom < zoomMin || tempZoom > zoomMax)
            {
                // nothing
            }
            else
            {
                zoom = tempZoom;
                Camera.main.orthographicSize = zoom;
            }                
        }        
    }
}
