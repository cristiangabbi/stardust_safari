using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static bool isGamePaused = false;
    InventoryController inventory;

    public GameObject pauseMenu;
    public Button photoIcon;
    public GameObject viewArea;

    CursorLockMode previousLockedMode;

    private void Start()
    {
        inventory = new InventoryController(photoIcon, viewArea);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
                this.Pause();
            else
                this.Unpause();
        }
    }


    public void Pause()
    {
        //show pause menu
        pauseMenu.SetActive(true);

        //stop time
        Time.timeScale = 0f;

        //set new cursor locked mode
        previousLockedMode = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;

        //set cursor mode
        isGamePaused = true;
    }

    public void Unpause()
    {
        //show pause menu
        pauseMenu.SetActive(false);

        //stop time
        Time.timeScale = 1f;

        //reset cursor locked mode
        Cursor.lockState = previousLockedMode;

        //set cursor mode
        isGamePaused = false;
    }

    public void ShowInventory()
    {
        inventory.UpdateInventory();
    }

    public void ClearInventory()
    {
        inventory.DeleteInventory();
    }
}
