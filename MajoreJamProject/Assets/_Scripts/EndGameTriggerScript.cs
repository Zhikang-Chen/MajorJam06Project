using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGameTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            StartCoroutine(ApplyScene());
    }

    private IEnumerator ApplyScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("WinScreen", LoadSceneMode.Additive);
    }
}
