using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAudioVolume : MonoBehaviour {

    public Slider Volume;
    public AudioSource myAudio;
        // Update is called once per frame
    void Update ()
    {
        myAudio.volume = Volume.value;
        if (!Volume)
        {
            this.enabled = false;
        }
    }

    public void Deactivethescript()
    {
        this.enabled = false;
    }
}
