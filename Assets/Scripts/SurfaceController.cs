using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceController : MonoBehaviour
{
    SurfaceInteractionHandler surfaceInteraction;

    public GameObject vanUI;
    public GameObject FPPlayerUI;

    public GameObject spaceship;
    public GameObject spaceman;
    public GameObject spacemanCharacter;
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    public Vector3 spacemanExitPositionOffset;

    [SerializeField]
    public static bool isAllowedToExitSpaceship { get; set; }
    [SerializeField]
    public static bool isAllowedToEnterSpaceship { get; set; }

    //Audio manager
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        isAllowedToExitSpaceship = false;
        isAllowedToEnterSpaceship = false;
        audioManager = FindObjectOfType<AudioManager>();
        surfaceInteraction = new SurfaceInteractionHandler(spaceship, spaceman, spacemanCharacter, firstPersonCamera, thirdPersonCamera, spacemanExitPositionOffset, audioManager);

    }

    // Update is called once per frame
    void Update()
    {
        if(isAllowedToExitSpaceship && !PauseMenuController.isGamePaused)
        {
            if (!vanUI.activeSelf)
            {
                vanUI.SetActive(true);
                FPPlayerUI.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                surfaceInteraction.GetOffSpaceship();
                isAllowedToExitSpaceship = false;
            }
        }
        else if(isAllowedToEnterSpaceship && !PauseMenuController.isGamePaused)
        {
            if (!FPPlayerUI.activeSelf)
            {
                FPPlayerUI.SetActive(true);
                vanUI.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                surfaceInteraction.GetOnSpaceship();
                isAllowedToEnterSpaceship = false;
            }
        }
        else
        {
            FPPlayerUI.SetActive(false);
            vanUI.SetActive(false);
        }
    }
}
