using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUIManager : MonoBehaviour
{
    static public UnityAction<float> OnStaminaUpdate = null;
    static public UnityAction OnPauseGame = null;

    [Header("UI Panels")]
    [SerializeField]
    private GameObject PauseMenu = null;

    [SerializeField]
    private GameObject SettingMenu = null;

    // make each button into their own class later
    // for now they are in here
    // and have to be assign by hand
    [Header("UI Element")]
    [SerializeField]
    private Slider staminaBar = null;

    [SerializeField]
    private Button ResumeButton = null;

    [SerializeField]
    private Button SettingButton = null;

    [SerializeField]
    private Button BackButton = null;

    [SerializeField]
    private Button QuitButton = null;

    [SerializeField]
    private Slider SFXSlider = null;

    [SerializeField]
    private Slider MusicSlider = null;

    private void Awake()
    {
        OnStaminaUpdate = UpdateStaminaBar;
        OnPauseGame = OpenPauseMenu;


        ResumeButton.onClick.AddListener(ClosePauseMenu);
        BackButton.onClick.AddListener(CloseSettingMenu);
        SettingButton.onClick.AddListener(OpenSettingMenu);
        QuitButton.onClick.AddListener(Quit);

        SFXSlider.onValueChanged.AddListener(ChangeSFX);
        MusicSlider.onValueChanged.AddListener(ChangeMusic);
    }

    // Start is called before the first frame update
    void Start()
    {
        staminaBar.gameObject.SetActive(false);
        SFXSlider.value = AudioManager.Instance.SFXVolume;
        MusicSlider.value = AudioManager.Instance.MusicVolume;
    }

    void UpdateStaminaBar(float v)
    {
        if(staminaBar)
        {
            staminaBar.gameObject.SetActive(v <= 1);
            staminaBar.value = v;
        }
    }

    void OpenPauseMenu()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        if (PauseMenu)
        {
            PauseMenu.SetActive(true);
        }
    }

    void ClosePauseMenu()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        if (PauseMenu)
        {
            PauseMenu.SetActive(false);
        }
    }

    void OpenSettingMenu()
    {
        SettingMenu.SetActive(true);
    }

    void CloseSettingMenu()
    {
        SettingMenu.SetActive(false);
    }

    void ChangeSFX(float v)
    {
        AudioManager.Instance.SFXVolume = v;
    }

    void ChangeMusic(float v)
    {
        AudioManager.Instance.MusicVolume = v;
    }

    void Quit()
    {
        Application.Quit();
    }
}
