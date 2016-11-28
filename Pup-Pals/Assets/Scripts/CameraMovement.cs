using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public float movementZone = 30;
    public float movementSpeed = 0.5f;

    public float xMax = 11.37f;
    public float xMin = -11.28f;

    public float yMax = 4.83f;
    public float yMin = -4.96f;

    private Vector3 desiredPosition;

    void Start()
    {
        desiredPosition = transform.position;
    }

	void Update ()
    {
        float x = 0, y =0, z = 0;
        float speed = movementSpeed * Time.deltaTime;

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
            

        Vector3 move = new Vector3(x, y, z) + desiredPosition;
        move.x = Mathf.Clamp(move.x, xMin, xMax);
        move.y = Mathf.Clamp(move.y, yMin, yMax);
        desiredPosition = move;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.2f);

    }
}
