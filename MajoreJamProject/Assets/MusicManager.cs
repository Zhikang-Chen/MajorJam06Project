using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public GameObject Alein;
    public GameObject Player;

    private void Update()
    {
        var dist = Vector3.Distance(Alein.transform.position, Player.transform.position);

        if(dist < 10f)
        {
            AudioManager.Instance.StopMusic("Music_Roam");
            AudioManager.Instance.PlayMusic("Music_Chase");
        } else
        {
            AudioManager.Instance.StopMusic("Music_Chase");
            AudioManager.Instance.PlayMusic("Music_Roam");
        }
    }
}
