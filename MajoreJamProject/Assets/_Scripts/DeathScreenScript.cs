using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreenScript : MonoBehaviour
{
    [SerializeField]
    private Button restartButton;

    [SerializeField]
    private Button exitButton;

    private void Awake()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;

        restartButton.onClick.AddListener(Restart);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void Restart()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("SampleScene");
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
