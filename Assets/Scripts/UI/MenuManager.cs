using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static bool isGamePaused = false;

    public GameObject pauseMenu;
    public Camera menuCamera;
    Camera activeCamera;

    CursorLockMode cursorCurrentState;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        //show pause menu
        activeCamera = Camera.current;
        activeCamera.gameObject.SetActive(false);
        menuCamera.gameObject.SetActive(true);
        pauseMenu.SetActive(true);

        //stop time
        Time.timeScale = 0f;

        //set cursor mode
        cursorCurrentState = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;

        isGamePaused = true;
    }

    void Resume()
    {
        activeCamera.gameObject.SetActive(true);
        menuCamera.gameObject.SetActive(false);
        pauseMenu.SetActive(false);

        //stop time
        Time.timeScale = 1f;

        //set cursor mode
        Cursor.lockState = cursorCurrentState;

        isGamePaused = false;
    }
}
