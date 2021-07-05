using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject pauseMenuPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Unpause();
            else Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenuPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        pauseMenuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }
}
