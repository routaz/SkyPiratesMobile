using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader leveLoader;
    public int currentSceneIndex;

    void Awake()
    {
        if (leveLoader == null)
        {
            DontDestroyOnLoad(gameObject);
            leveLoader = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    

    public void LoadNextScene()
    {
        if (GameManager.gameManager.GetPlayerLives() != 0)
        {
            //Debug.Log("LoadingNextScene");
            //Debug.Log(currentSceneIndex);
            //Debug.Log("NextScenePressed");
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("NoPlanes");
        }
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("TheGame");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Pressed");
        GameManager.gameManager.SaveGame();
        Application.Quit();
    }

    public void Restart()
    {
        Debug.Log("ReStartPressed");
        GameManager.gameManager.LoadSavedGame();
        if (GameManager.gameManager.GetPlayerLives() <= 0)
        {
            SceneManager.LoadScene("NoPlanes");
            //Debug.Log("lives" + GameManager.gameManager.GetPlayerLives());
            //FindObjectOfType<SessionManager>().summaryPanel.SetActive(false);
            //FindObjectOfType<SessionManager>().noMoreLivesPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Lataa Levelin Uusiks");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //FindObjectOfType<GameManager>().roundsOfGamePlayed++;
        }

    }

    public void Repaired()
    {
        Debug.Log("RepairedPressed");
        SceneManager.LoadScene("Repaired");
    }

    public void MainMenu()
    {
        Debug.Log("MainMenuPressed");
        SceneManager.LoadScene("MainMenu");
    }

    public void About()
    {
        Debug.Log("About Pressed");
        SceneManager.LoadScene("About");
    }

    public void Options()
    {
        Debug.Log("Options Pressed");
        SceneManager.LoadScene("OptionsScreen");
    }

    public void SignIn()
    {
        SceneManager.LoadScene("SingInToGoogle");
    }

}
