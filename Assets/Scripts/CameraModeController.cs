using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraModeController : MonoBehaviour
{
    public Camera screenshotCamera, mainFPCamera;

    bool isPhotoModeActive = false;

    void Update()
    {
        if (!isPhotoModeActive)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                GetComponent<FirstPersonController>().enabled = false;
                GetComponent<ScreenshotController>().enabled = true;

                //switch between audio listener
                mainFPCamera.gameObject.GetComponent<AudioListener>().enabled = false;
                screenshotCamera.gameObject.GetComponent<AudioListener>().enabled = true;

                //switch between cameras
                mainFPCamera.gameObject.SetActive(false);
                screenshotCamera.gameObject.SetActive(true);

                //set appropriate camera in appropriate position
                screenshotCamera.transform.eulerAngles = mainFPCamera.transform.eulerAngles;

                isPhotoModeActive = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                GetComponent<FirstPersonController>().enabled = true;
                GetComponent<ScreenshotController>().enabled = false;

                //switch between audio listener
                screenshotCamera.gameObject.GetComponent<AudioListener>().enabled = false;
                mainFPCamera.gameObject.GetComponent<AudioListener>().enabled = true;

                //switch between cameras
                screenshotCamera.gameObject.SetActive(false);
                mainFPCamera.gameObject.SetActive(true);

                //set appropriate camera in appropriate position
                mainFPCamera.transform.eulerAngles = screenshotCamera.transform.eulerAngles;

                isPhotoModeActive = false;
            }
        }

    }
}
