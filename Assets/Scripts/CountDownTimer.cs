using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CountDownTimer : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;
    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(CountdownToStart());
    }
    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownDisplay.text = "Fly!";

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("BACKUP");
    
    }

}
