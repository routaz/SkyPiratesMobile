using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class SessionManager : MonoBehaviour
{
    
    [SerializeField] public GameObject summaryPanel;
    //[SerializeField] public GameObject noMoreLivesPanel;
    [SerializeField] public GameObject gameCanvas;

    [SerializeField] int countdownTime;
    [SerializeField] TextMeshProUGUI countdownDisplay;

    [SerializeField] TextMeshProUGUI playerLivesLeftText;

    [SerializeField] float delayTime;

    public AudioSource audioSource;
    public AudioClip countDownHits;

    public bool gameHasStarted = false;
    public bool gameHasEnded = false;


    public void Start()
    {
        summaryPanel.SetActive(false);
        //noMoreLivesPanel.SetActive(false);
        StartCoroutine(CountdownToStart());
        GameManager.gameManager.LoadSavedGame();
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator CountdownToStart()
    {
        audioSource.PlayOneShot(countDownHits);

        while (countdownTime > 0)
        {
            
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownDisplay.text = "Fly!";

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
        gameHasStarted = true;
        StartGame();


    }


    private void Update()
    {
        PlayerLives();
    }


    public void LoadGame()
    {
        if (GameManager.gameManager.playerLives <= 0)
        {
            LevelLoader.leveLoader.Restart();
        }
        else
        {
            SceneManager.LoadScene("TheGame");
        }

    }

    public void StartGame()
    {

        summaryPanel.SetActive(false);
        gameCanvas.SetActive(true);

        FindObjectOfType<PlaneSpawner>().Spawn();
        FindObjectOfType<BalloonSpawner>().isSpawning = true;
        FindObjectOfType<FuelSpawner>().isSpawning = true;
        FindObjectOfType<CoinSpawner>().isSpawning = true;

    }

    public void EndGame()
    {
        if (!gameHasEnded)
        {
            GameManager.gameManager.playerLives -= 1;
            GameManager.gameManager.SaveGame();
            
            gameHasEnded = true;
            gameHasStarted = false;
            Invoke("GameOver", delayTime);
        }
    }

    public void GameOver()
    {
        FindObjectOfType<BalloonSpawner>().isSpawning = false;
        FindObjectOfType<FuelSpawner>().isSpawning = false;
        FindObjectOfType<CoinSpawner>().isSpawning = false;

        gameCanvas.SetActive(false);
        summaryPanel.SetActive(true);

        var distance = FindObjectOfType<PlayerDistance>().GetDistanceAmount();
        GooglePlayServicesManager.googlePlayServicesManager.SendDistanceToLeaderboard(distance);

    }

    public void PlayerLives()
    {
        if (playerLivesLeftText == null) { return; }
        else
        {
            playerLivesLeftText.text = GameManager.gameManager.playerLives.ToString("");
        }
    }



}
