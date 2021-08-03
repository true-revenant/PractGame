using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && !settingsPanel.activeSelf) Unpause();
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
