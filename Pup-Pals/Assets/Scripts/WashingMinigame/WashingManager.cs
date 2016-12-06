using UnityEngine;

public class WashingManager : MonoBehaviour {

    public float radius;
    public Color InitialColor;
    public GameObject puppet;
    public int maxPixelChange;
    public float percentageRequired;
    public float cleaningRate;

    private int[,] changedPixels;
    private RaycastHit2D hitInfo;
    private int width;
    private int height;

    private void Start()
    {
        width = puppet.GetComponent<SpriteRenderer>().sprite.texture.width;
        height = puppet.GetComponent<SpriteRenderer>().sprite.texture.height;
        changedPixels = new int[width, height];
    }
    // Update is called once per frame
    void Update()
    {
        if (checkIfDone())
        {
            // The player finished.
            // plz go back to game scene now.
            Debug.Log("You done boi");
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

    public Texture2D CopyTexture2D(Texture2D copiedTexture2D)
    {
        float differenceX;
        float differenceY;

        //Create a new Texture2D, which will be the copy
        Texture2D texture = new Texture2D(width, height);

        //Choose your filtermode and wrapmode
        texture.filterMode = FilterMode.Bilinear;
        texture.wrapMode = TextureWrapMode.Clamp;

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

                        // new color;
                        pixelColor[0] += cleaningRate;
                        pixelColor[1] += cleaningRate;
                        pixelColor[2] += cleaningRate;

                        texture.SetPixel(x, y, pixelColor);
                        // incrementing the pixel. 
                        changedPixels[x, y] += 1;
                    }
                    // if its been changed we have to copy the original pixel over to the new texture
                    else
                    {
                        texture.SetPixel(x, y, copiedTexture2D.GetPixel(x, y));
                    }
                }
                else
                {
                    // copies the original pixel to the new texture
                    texture.SetPixel(x, y, copiedTexture2D.GetPixel(x, y));
                }
            }
        }

        // Lets apply all our changes
        texture.Apply();
        return texture;
    }

    public void UpdateTexture(bool clicked)
    {
        SpriteRenderer mySpriteRenderer = puppet.GetComponent<SpriteRenderer>();
        Texture2D newTexture2D;
        if (clicked)
        {
            newTexture2D = CopyTexture2D(mySpriteRenderer.sprite.texture);
        }
        else
        {
            newTexture2D = mySpriteRenderer.sprite.texture;
        }

        //Get the name of the old sprite
        string tempName = mySpriteRenderer.sprite.name;
        //Create a new sprite
        mySpriteRenderer.sprite = Sprite.Create(newTexture2D, mySpriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f));
        //Name the sprite, the old name
        mySpriteRenderer.sprite.name = tempName;
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
