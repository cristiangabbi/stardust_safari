using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipPointerController : MonoBehaviour
{
    public Transform van;
    public Transform player;
    public Camera cam;
    public Vector2 offset;
    public Text distance;
    public Image image;

    // Update is called once per frame
    void Update()
    {
        float minX = image.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = image.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = cam.WorldToScreenPoint(van.position);

        //check if spaceship is behind
        if (Vector3.Dot(van.position - player.position, player.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
                pos.x = maxX;
            else
                pos.x = minX;

        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        image.transform.position = pos + offset;
        distance.text = Vector3.Distance(van.position, player.position).ToString() + "m";
    }
}
