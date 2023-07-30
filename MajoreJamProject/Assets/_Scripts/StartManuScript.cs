using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManuScript : MonoBehaviour
{
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button ExitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayGame);
        ExitButton.onClick.AddListener(ExitGame);
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("Title_2");
    }

    void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
