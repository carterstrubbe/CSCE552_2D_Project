using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public AudioSource src;
    public AudioClip openMenuSound;
    public GameObject gameOverUI;
    public GameObject menu;
    bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) {
            src.clip = openMenuSound;
            src.Play();
            if (paused) {
                Time.timeScale = 1.0f;
				menu.SetActive(false);
				paused = false;
            } else {
                Time.timeScale = 0.0f;
                menu.SetActive(true);
                paused = true;
            }
        }
        
    }

    public void GameOver() {
        gameOverUI.SetActive(true);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (paused) {
            Time.timeScale = 1.0f;
            menu.SetActive(false);
            paused = false;
        }
    }

    public void Exit() {
        Debug.Log("QUitting)");
        Application.Quit();
    }

    public void Resume() {
        Time.timeScale = 1.0f;
        menu.SetActive(false);
        paused = false;
    }
}
