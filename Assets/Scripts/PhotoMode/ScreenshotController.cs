using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScreenshotController : MonoBehaviour
{
    public Camera screenshotCamera;
    public Transform character;
    public float mouseSensitivity = 100f;
    public int texHeight, texWidth;
    int lastPicCount;

    public Texture2D renderResult;

    float rotationX = 0f;
    float rotationY = 0f;

    void Start()
    {
        try
        {
            string s = System.IO.File.ReadAllText(Application.persistentDataPath + "/Camera/lastCount.txt");

            if (s != null)
            {
                int.TryParse(s, out lastPicCount);
            }
            else
            {
                lastPicCount = 0;
            }
        }
        catch(Exception e)
        {
            lastPicCount = 0;
            Debug.LogWarning("Something went wrong: " + e.Message);
        }
    }


    private void Update()
    {
        MoveCamera();

        if(Input.GetKeyDown(KeyCode.L))
        {
            //other photo mode stuff
            TakePicture();
            Debug.Log("Picture taken");
        }
    }


    IEnumerator PerformScreenshot()
    {
        //tell the camera to not render the UI layer
        screenshotCamera.cullingMask = screenshotCamera.cullingMask & ~(1 << LayerMask.NameToLayer("UI"));

        //play sound
        FindObjectOfType<AudioManager>().Play("Photo");

        // We should only read the screen buffer after rendering is complete
        yield return new WaitForEndOfFrame();

        RenderTexture rt = RenderTexture.GetTemporary(Screen.width, Screen.height, 16);
        renderResult = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);

        //Rect areaToBeTaken = new Rect(Screen.width / 2 - rt.width / 2, 0, rt.width, rt.height);
        Rect areaToBeTaken = new Rect(0, 0, rt.width, rt.height);

        renderResult.ReadPixels(areaToBeTaken, 0, 0);

        //store in image
        byte[] byteArray = renderResult.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.persistentDataPath + "/Camera/Shot" + lastPicCount + ".png", byteArray);

        lastPicCount++;
        System.IO.File.WriteAllText(Application.persistentDataPath + "/Camera/lastCount.txt", lastPicCount.ToString());

        //make UI visible again
        screenshotCamera.cullingMask = screenshotCamera.cullingMask | (1 << LayerMask.NameToLayer("UI"));

        //clear renderTexture
        RenderTexture.ReleaseTemporary(rt);
    }


    void TakePicture()
    {
        StartCoroutine("PerformScreenshot");
    }




    void MoveCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -70f, 70f);

        rotationY += mouseX;

        //camera local rotation
        screenshotCamera.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        //character local rotation
        character.localRotation = Quaternion.Euler(0, rotationY, 0);
    }


}
