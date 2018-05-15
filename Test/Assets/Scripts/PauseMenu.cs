using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject[] OpenPanels;

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
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
                //go.GetComponent<TouchScript>().enabled = false;
                //go.collider.enabled = false;
            }
        }
    }

    public void LoadMainScreen()
    {
        Debug.Log("Loading Main screen...");
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void Quit()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
