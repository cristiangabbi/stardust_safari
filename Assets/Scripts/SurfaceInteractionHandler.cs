using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SurfaceInteractionHandler
{
    GameObject spaceship;
    GameObject spaceman;
    GameObject spacemanCharacter;
    Camera firstPersonCamera;
    Camera thirdPersonCamera;
    Vector3 spacemanExitPositionOffset;
    AudioManager audioManager;

    public SurfaceInteractionHandler(GameObject spaceship,
                                    GameObject spaceman,
                                    GameObject spacemanCharacter,
                                    Camera firstPersonCamera, 
                                    Camera thirdPersonCamera, 
                                    Vector3 spacemanExitPositionOffset,
                                    AudioManager audioManager)
    {
        this.spaceship = spaceship;
        this.spaceman = spaceman;
        this.spacemanCharacter = spacemanCharacter;
        this.firstPersonCamera = firstPersonCamera;
        this.thirdPersonCamera = thirdPersonCamera;
        this.spacemanExitPositionOffset = spacemanExitPositionOffset;
        this.audioManager = audioManager;
    }

    public void GetOnSpaceship()
    {
        // enable/disable controllers
        spaceship.GetComponent<PlayerController>().enabled = true;

        //disable spaceman
        spaceman.SetActive(false);

        //switch camera listener
        thirdPersonCamera.GetComponent<AudioListener>().enabled = false;
        thirdPersonCamera.GetComponent<AudioListener>().enabled = true;

        //switch camera
        firstPersonCamera.enabled = false;
        thirdPersonCamera.enabled = true;

        //change mouse state
        Cursor.lockState = CursorLockMode.None;

        //sounds
        audioManager.Play("SpaceshipSound");
        audioManager.Stop("Footsteps");

        if (!audioManager.IsPlayingAmbient())
            audioManager.PlayNextAmbientSound();
    }

    public void GetOffSpaceship()
    {
        // enable/disable controllers
        spaceship.GetComponent<PlayerController>().enabled = false;

        //switch camera listener
        thirdPersonCamera.GetComponent<AudioListener>().enabled = false;
        firstPersonCamera.GetComponent<AudioListener>().enabled = true;

        //switch camera
        thirdPersonCamera.enabled = false;
        firstPersonCamera.enabled = true;

        //set spaceman position and offset
        spaceman.transform.position = spaceship.transform.position +
                                    spaceship.transform.right.normalized * spacemanExitPositionOffset.x +
                                    spaceship.transform.up.normalized * spacemanExitPositionOffset.y;

        //rotate spaceman
        //spaceman.transform.eulerAngles = spaceship.transform.eulerAngles;
        spaceman.transform.eulerAngles = spaceship.transform.eulerAngles;


        //activate spaceman
        spaceman.SetActive(true);

        //change mouse state
        Cursor.lockState = CursorLockMode.Locked;

        //sounds
        audioManager.Stop("SpaceshipSound");
        audioManager.Play("Footsteps");
    }
}
