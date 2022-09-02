using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconButton : MonoBehaviour
{
    [HideInInspector]
    public Texture2D tex;
    public RawImage image;
    public GameObject fullscreenPanel;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ShowFullImage);
    }

    public void ShowFullImage()
    {
        image.texture = tex;
        fullscreenPanel.SetActive(true);
    }

    
}
