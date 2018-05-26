using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    private AudioManager music;
    public Button musicButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    void Start()
    {
        music = GameObject.FindObjectOfType<AudioManager>();
        UpdateIconAndVolume();
    }

    public void PauseMusic()
    {
        music.ToggledSound(); //update our player prefs
        UpdateIconAndVolume();
    }

    public void UpdateIconAndVolume()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            AudioListener.volume = 1;
            musicButton.GetComponent<Image>().sprite = musicOnSprite;
        }
        else
        {
            AudioListener.volume = 0;
            musicButton.GetComponent<Image>().sprite = musicOffSprite;
        }
    }

    public void QuitGame(){
        Application.Quit();
    }

	public void ResetMemory(){
		MemoryScript memory = GameObject.FindObjectOfType<MemoryScript>();
		if (memory != null) {
			memory.setLastLevelCompleted (0);
			memory.Save ();
		}
	}


}
