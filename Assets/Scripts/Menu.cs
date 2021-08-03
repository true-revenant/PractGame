using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

internal sealed class Menu : MonoBehaviour
{
    [SerializeField] private Button btnStart;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnCloseSettings;
    [SerializeField] private Button btnExit;
    [SerializeField] private Slider soundEffectsVolume;
    [SerializeField] private Slider musicVolume;

    [SerializeField] private GameObject panelSettings;

    private AudioSource backMusicAudioSource;
    private SoundController soundController;

    private void Awake()
    {
        backMusicAudioSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        soundController = GameObject.Find("SoundManager").GetComponent<SoundController>();

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
        // ENEMIES
        SetVolumeToObjsByTag("Enemy", value);

        // ENEMIE PATROLS
        SetVolumeToObjsByTag("EnemyPatrol", value);

        // Turrets
        SetVolumeToObjsByTag("Turret", value);

        // Player
        SetVolumeToObjsByTag("Player", value);

        // Boss
        SetVolumeToObjsByTag("Boss", value);

        // Buttons
        SetVolumeToObjsByTag("Button", value);

        soundController.EffectsVolume = value;

        Debug.Log("Sound Effects Volume Changed!");
    }

    private void SetVolumeToObjsByTag(string tag, float val)
    {
        GameObject[] soundingGameObjects = GameObject.FindGameObjectsWithTag(tag);
        if (soundingGameObjects != null)
        {
            for (int i = 0; i < soundingGameObjects.Length; i++)
            {
                soundingGameObjects[i].GetComponent<AudioSource>().volume = val;
            }
        }
    }

    private void SetMusicEffectsVolume(float value)
    {
        Debug.Log("Music Volume Changed!");
        backMusicAudioSource.volume = value;
    }
}
