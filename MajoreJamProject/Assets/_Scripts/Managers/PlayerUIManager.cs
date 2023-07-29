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

    [SerializeField]
    private Slider staminaBar = null;

    [SerializeField]
    private GameObject PauseMenu = null;

    // make each button into their own class later
    // for now they are in here
    // and have to be assign by hand
    [SerializeField]
    private Button ResumeButton;

    [SerializeField]
    private Button SettingButton;

    [SerializeField]
    private Button QuitButton;

    private void Awake()
    {
        OnStaminaUpdate = UpdateStaminaBar;
        OnPauseGame = OpenPauseMenu;


        ResumeButton.onClick.AddListener(ClosePauseMenu);
        QuitButton.onClick.AddListener(Quit);
    }

    // Start is called before the first frame update
    void Start()
    {
        staminaBar.gameObject.SetActive(false);
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

    void Quit()
    {
        Application.Quit();
    }
}
