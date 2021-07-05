using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button btnStart;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnCloseSettings;
    [SerializeField] private Button btnExit;
    [SerializeField] private Slider soundEffectsVolume;
    [SerializeField] private Slider musicVolume;

    [SerializeField] private GameObject panelSettings;

    private void Awake()
    {
        Debug.Log("MENU AWAKE!");
        btnStart.onClick.AddListener(StartGame);
        btnSettings.onClick.AddListener(ShowSettings);
        btnCloseSettings.onClick.AddListener(CloseSettings);
        btnExit.onClick.AddListener(EndGame);
        soundEffectsVolume.onValueChanged.AddListener(SetSoundEffectsVolume);
        musicVolume.onValueChanged.AddListener(SetMusicEffectsVolume);
    }

    private void Start()
    {
        Debug.Log("MENU START!");
    }

    private void StartGame()
    {
        Debug.Log("START Main Level!");
        SceneManager.LoadScene("MainLevel");
    }

    private void EndGame()
    {
        Application.Quit();
    }

    private void ShowSettings()
    {
        Debug.Log("SHOW SETTINGS WINDOW!");
        panelSettings.SetActive(true);
    }

    private void CloseSettings()
    {
        panelSettings.SetActive(false);
    }

    private void SetSoundEffectsVolume(float value)
    {
        Debug.Log("Sound Effects Volume Changed!");
    }

    private void SetMusicEffectsVolume(float value)
    {
        Debug.Log("Music Volume Changed!");
    }
}
