using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {


    public float movementZone = 30;
    public float movementSpeed = 0.1f;

    public float mapX;
    public float mapY;

    private float zoom = 5;
    private float zoomSpeed = 0.2f;
    private float zoomMax = 10;
    private float zoomMin = 2;

    public SpriteRenderer spriteBounds;
    
    private Vector3 desiredPosition;

    void Start()
    {
        desiredPosition = transform.position;
    }

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
        else
        {
            // mouse movement
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
