using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAudioManager : MonoBehaviour
{
    public float SoundIntervals;
    public GameObject player;

    public AudioSource _audio;
    public AudioClip Close;
    public AudioClip far;

    private bool _playSound;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        _audio.volume = AudioManager.Instance.SFXVolume;

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
            _audio.PlayOneShot(Close);
        } else
        {
            _audio.PlayOneShot(far);
        }

        _playSound = false;
        Invoke("SfxInterval", SoundIntervals);
    }

    public void SfxInterval() => _playSound = true;
}
