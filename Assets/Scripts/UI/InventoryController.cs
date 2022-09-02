using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryController
{
    Button photoIcon;
    GameObject viewArea;
    int picturesTaken = 0;

    Texture2D[] takenPhotos;
    Button[] icons;

    public InventoryController(Button photoIcon, GameObject viewArea)
    {
        this.photoIcon = photoIcon;
        this.viewArea = viewArea;

        System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/Camera");
    }

    public void UpdateInventory()
    {
        getNumberOfPicturesTaken();
        LoadPictures();
        CreateIcons();
    }

    public void DeleteInventory()
    {
        foreach(Transform child in viewArea.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    bool getNumberOfPicturesTaken()
    {
        try
        {
            string s = System.IO.File.ReadAllText(Application.persistentDataPath + "/Camera/lastCount.txt");

            int.TryParse(s, out picturesTaken);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
            return false;
        }
    }

    void LoadPictures()
    {
        takenPhotos = new Texture2D[picturesTaken];

        for (int i = 0; i < picturesTaken; i++)
        {
            byte[] byteArray = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/Camera/Shot" + i + ".png");
            Texture2D tex = new Texture2D(Screen.width /2, Screen.height /2);
            tex.LoadImage(byteArray);
            tex.Apply();

            takenPhotos[i] = tex;
        }

        Debug.Log(takenPhotos.Length + " textures created");
    }

    void CreateIcons()
    {
        icons = new Button[picturesTaken];

        for(int i = 0; i < picturesTaken; i++)
        {
            icons[i] = GameObject.Instantiate(photoIcon);            
            icons[i].transform.SetParent(viewArea.transform, false);
            icons[i].gameObject.SetActive(true);

            //set textures
            if (takenPhotos.Length == icons.Length)
            {
                icons[i].image.sprite = Sprite.Create(takenPhotos[i], new Rect(0, 0, 200, 200), new Vector2(0.5f, 0.5f) );

                icons[i].GetComponent<IconButton>().tex = takenPhotos[i];
            }
        }
    }


}
