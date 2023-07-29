using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoundLibrary : ScriptableObject
{
    [System.Serializable]
    public class Sound
    {
        public string ID;
        public AudioClip Audio;
        //public float PlayTime = 0;
        //public float EndTime = -1;
    }

    public List<Sound> Sounds;
    public List<Sound> Music;
}
