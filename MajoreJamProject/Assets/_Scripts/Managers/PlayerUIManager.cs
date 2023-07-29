using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUIManager : MonoBehaviour
{
   static public UnityAction<float> OnStaminaUpdate = null;

    [SerializeField]
    private Slider staminaBar = null;

    private void Awake()
    {
        OnStaminaUpdate = UpdateStaminaBar;
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
}
