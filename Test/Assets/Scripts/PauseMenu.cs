using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject[] OpenPanels;
    private AudioManager music;
    public Button musicButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        if (OpenPanels != null)
        {
            foreach (GameObject go in OpenPanels)
            {
                go.SetActive(true);
                //go.GetComponent<TouchScript>().enabled = false;
                //go.collider.enabled = false;
            }
        }
       // music.myMusic.volume = 1f;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        OpenPanels = GameObject.FindGameObjectsWithTag("InstructionsPanel");
        if (OpenPanels != null)
        {
            foreach (GameObject go in OpenPanels)
            {
                go.SetActive(false);
            }
        }
        music = GameObject.FindObjectOfType<AudioManager>();
        UpdateIconAndVolume();
       // music.myMusic.volume = 0.5f;
    }

    public void LoadMainScreen()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("LevelsMenu");
    }

    public void Quit()
    {
        MemoryScript memory = GameObject.FindObjectOfType<MemoryScript>();
        if (memory != null)
        {
            memory.Save();
        }
        Application.Quit();
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
}
