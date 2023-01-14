using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused=false;
    public GameObject pausedMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        pausedMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;

        pausedMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Paused()
    {
        Cursor.lockState = CursorLockMode.None;

        pausedMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
