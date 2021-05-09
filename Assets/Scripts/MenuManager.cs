using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


    public void LoadMainGame()
    {
        Debug.Log(GameManager.gameManager.playerLives);
    
        if (GameManager.gameManager.playerLives == 0)
        {
            SceneManager.LoadScene("NoPlanes");
        }
        else
        {
            SceneManager.LoadScene("TheGame");
        }
    }
}
