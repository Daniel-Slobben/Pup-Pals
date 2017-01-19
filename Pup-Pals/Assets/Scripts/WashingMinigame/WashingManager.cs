using UnityEngine;
using UnityEngine.UI;

public class WashingManager : MonoBehaviour {
        
    public GameObject puppet;
    public GameObject dirtyPuppet;
    public float radius;
    public int maxPixelChange;
    public float percentageRequired;
    public float cleaningRate;
    public Texture2D cursorTexture;

    private int[,] changedPixels;
    private RaycastHit2D hitInfo;
    private int width;
    private int height;

    private void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        width = puppet.GetComponent<SpriteRenderer>().sprite.texture.width;
        height = puppet.GetComponent<SpriteRenderer>().sprite.texture.height;
        changedPixels = new int[width, height];

    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(width * height * percentageRequired);
        if (checkIfDone())
        {
            // The player finished.
            // plz go back to game scene now.
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            LoadSavePlayer loadSavePlayer = GetComponent<LoadSavePlayer>();
            loadSavePlayer.loadPlayer(SaveLoadController.control.playerIdentity);
            //Application.LoadLevel("game");
        }
        if (Input.GetMouseButton(0))
        {
            
            hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hitInfo)
            {
                UpdateTexture(true);
                
            }
            else
            {
                UpdateTexture(false);
            }
        }
        else
        {
            UpdateTexture(false);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Resources.UnloadUnusedAssets();
        }
    }

    /**
     * This function put a circle around the mouse and changes the color values of all the pixels inside the circle
     */
    public Texture2D CopyTexture2D(Texture2D copiedTexture2D)
    {
        float differenceX;
        float differenceY;

        // Create a copy, i made sure it wasnt a reference. 
        Texture2D texture = Instantiate(copiedTexture2D) as Texture2D;

        //Center of hit point circle 
        int m1 = (int)((hitInfo.point.x - hitInfo.collider.bounds.min.x) * (width / hitInfo.collider.bounds.size.x));
        int m2 = (int)((hitInfo.point.y - hitInfo.collider.bounds.min.y) * (height / hitInfo.collider.bounds.size.y));

        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                differenceX = x - m1;
                differenceY = y - m2;
                
                // if the pixel is in the circle
                if (differenceX * differenceX + differenceY * differenceY <= radius * radius)
                {
                    // if the pixel hasnt been changed too many times
                    if (changedPixels[x, y] < maxPixelChange)
                    {
                        Color pixelColor = copiedTexture2D.GetPixel(x, y);

                        pixelColor.a -= cleaningRate;

                        texture.SetPixel(x, y, pixelColor);
                        // incrementing the pixel. 
                        changedPixels[x, y] += 1;
                    }
                }
            }
        }
        // Lets apply all our changes
        texture.Apply();
        return texture;
    }

    public void UpdateTexture(bool clicked)
    {
        SpriteRenderer spriteRendererPuppet = puppet.GetComponent<SpriteRenderer>();
        SpriteRenderer spriteRendererPuppetDirty = dirtyPuppet.GetComponent<SpriteRenderer>();
        Texture2D newTexture2D;
        newTexture2D = spriteRendererPuppet.sprite.texture;
        Texture2D newTexture2DDirty;
        if (clicked)
        {
            newTexture2DDirty = CopyTexture2D(spriteRendererPuppetDirty.sprite.texture);
        }
        else
        {
            newTexture2DDirty = spriteRendererPuppetDirty.sprite.texture;
        }
        //Get the name of the old sprite
        string tempNameDirty = spriteRendererPuppetDirty.sprite.name;
        string tempName = spriteRendererPuppet.sprite.name;
        //Create a new sprite
        spriteRendererPuppetDirty.sprite = Sprite.Create(newTexture2DDirty, spriteRendererPuppet.sprite.rect, new Vector2(0.5f, 0.5f));
        spriteRendererPuppet.sprite = Sprite.Create(newTexture2D, spriteRendererPuppet.sprite.rect, new Vector2(0.5f, 0.5f));
        //Name the sprite, the old name
        spriteRendererPuppetDirty.sprite.name = tempNameDirty;
        spriteRendererPuppet.sprite.name = tempName;
    }

    public bool checkIfDone()
    {
        int counter = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (changedPixels[i, j] == maxPixelChange)
                {
                    counter++;
                }
            }
        }
        if (width * height * percentageRequired < counter)
        {
            return true;
        }
        return false;
    }
}
