using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
    
    public static AudioManager instance;

    //public Sound[] sounds;
    public Slider mySlider;
    public AudioSource myMusic;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        if (PauseMenu.GameIsPaused)
        {
            myMusic.volume = 0.2f;
        }

       /* foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }*/

    }

    public void ToggledSound()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            PlayerPrefs.SetInt("Muted", 1);
            //AudioListener.volume = 1;
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
            //AudioListener.volume = 0;

        }

    }
    /*
    void Start() {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound:" + name + " not found!");
            return;
        }
            
        s.source.Play();
    }*/   
}
