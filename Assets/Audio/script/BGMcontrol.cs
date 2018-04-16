using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMcontrol : MonoBehaviour {

    public AudioClip MusicClip;
    public AudioClip Village;
    public AudioClip Forest;
    public AudioClip FrontlineBase;
    public AudioClip Ruins;

    public AudioClip ArcheryAttack;
    public AudioClip ClickButton;
    public AudioClip Drink;
    public AudioClip LevelUp;
    public AudioClip MagicAttack;
    public AudioClip MagicMissiles;
    public AudioClip MissionComplete;
    public AudioClip PickUpObject;
    public AudioClip Portal;
    public AudioClip SwingAttack;
    public AudioClip SwordAttack;
    public AudioClip Trading;

    public AudioSource BGMSource;
    public AudioSource soundSource;

    // Use this for initialization
    void Start () {
        GameManager.SetBGM();
        DontDestroyOnLoad(this.gameObject);
        BGMSource.clip = MusicClip;
        BGMSource.Play();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void SetBGM(string bgm)
    {
        GameManager.AudioManager.GetComponent<ChangeAudioVolume>().Deactivethescript(); 
        BGMSource.Stop();
        switch(bgm)
        { 
            case "Village": //done
                BGMSource.clip = Village;
                break;

            case "Forest": //done
                BGMSource.clip = Forest;
                break;

            case "FrontlineBase": //done
                BGMSource.clip = FrontlineBase;
                break;

            case "Ruins": //done
                BGMSource.clip = Ruins;
                break;

            default:
                break;
        }

        BGMSource.Play();


    }

    public void Playsound(string sound)
    {
        soundSource.volume = 0.1f;
        switch (sound)
        {
            case "ArcheryAttack":
                soundSource.clip = ArcheryAttack;
                break;

            case "ClickButton":
                soundSource.clip = ClickButton;
                break;

            case "Drink": 
                soundSource.clip = Drink;
                break;

            case "LevelUp": 
                soundSource.clip = LevelUp;
                break;

            case "MagicAttack":
                soundSource.clip = MagicAttack;
                break;

            case "MagicMissiles":
                soundSource.clip = MagicMissiles;
                break;

            case "MissionComplete": //done
                soundSource.clip = MissionComplete;
                break;

            case "PickUpObject": //done
                soundSource.clip = PickUpObject;
                break;

            case "Portal": //done
                soundSource.clip = Portal;
                break;

            case "SwingAttack":
                soundSource.clip = SwingAttack;
                break;

            default:
                break;
        }

        soundSource.Play();
    }

    }
