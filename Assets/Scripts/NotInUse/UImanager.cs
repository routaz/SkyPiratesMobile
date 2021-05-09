using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("Countdown");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Countdown");
    }

    public void Quit()
    {
        SceneManager.LoadScene("MENU");
    }
}
