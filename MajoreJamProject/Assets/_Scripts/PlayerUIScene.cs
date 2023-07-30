using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUIScene : MonoBehaviour
{

    // Check if the UI Scene has been loaded
    private void Start()
    {
        if(!SceneManager.GetSceneByName("PlayerUI").isLoaded)
        {
            SceneManager.LoadScene("PlayerUI", LoadSceneMode.Additive);
        }
    }
}
