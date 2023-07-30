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
        
    }

    private void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
