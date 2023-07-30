using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAudioManager : MonoBehaviour
{
    public float SoundIntervals;
    public GameObject player;

    private bool _playSound;

    // Update is called once per frame
    void Update()
    {
        if(_playSound)
        {
            ChooseSound();
        }
    }

    private void ChooseSound()
    {
        float Distance = Vector3.Distance(transform.position, player.transform.position);
        if(Distance < 8f)
        {
            AudioManager.Instance.PlayAudio3D("Alien_Close", transform.position);
        } else
        {
            AudioManager.Instance.PlayAudio3D("Alien_Far", transform.position);
        }

        _playSound = false;
        Invoke("SfxInterval", SoundIntervals);
    }

    public void SfxInterval() => _playSound = true;
}
