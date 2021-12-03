using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject mainScreen;


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        pauseScreen.SetActive(true);
        mainScreen.SetActive(false);
        Time.timeScale = 0;
    }

    public void StartGameClicked()
    {
        mainMenuScreen.SetActive(false);
        mainScreen.SetActive(true);
    }

    public void RestartClicked()
    {
        ResumeClicked();
        SceneManager.LoadScene(0);
    }

    public void ResumeClicked()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        mainScreen.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        mainScreen.SetActive(false);
        gameOverScreen.SetActive(true);
        
    }
}
