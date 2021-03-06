using System.Collections;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using GooglePlayGames;


public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    int currentSceneIndex;
    [SerializeField] float timeToWait = 4;

    //[SerializeField] int roundsToPlayBeforeadds = 5;
    //[SerializeField] public int roundsOfGamePlayed;
    [SerializeField] public int playerLives = 5;

    public float bestPlayerDistance;
    public int totalCoins;

    public DateTime currentTime;
    public DateTime coolDownTime;
    public DateTime loggedInTime;

    [SerializeField] float coolDownTimeAmount = 10;
    [SerializeField] int amountToRewardAfterTimer;
    [SerializeField] int amountIfPayedRepair = 2;

    public DateTime setCurrentTime;
    public DateTime setCoolDownTime;
    public bool shouldSetCoolDownTime;
    public bool timerStarted;

    public TimeSpan coolDownLeft;

    public int onGoingCoolDownTime;

    public bool isTutorialWatched;

    [Header("Achievements")]
    public int coinsCollectedAlltime;
    public float distanceTravelled;
    public int timesDiedFromCannon;
    public int timesDiedFromCrash;

    [SerializeField] string kamikazeAchi = "CgkIz8uioKkVEAIQCg";
    [SerializeField] string getRektAchi = "CgkIz8uioKkVEAIQCw";



    void Awake()
    {
        if (gameManager == null)
        {              
            DontDestroyOnLoad(gameObject);
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadSavedGame();
       
        //loggedInTime = System.DateTime.Now;
        //Debug.Log(loggedInTime);

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            StartCoroutine(WaitForTime());
        }
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait);
        if (!isTutorialWatched)
        {
            LevelLoader.leveLoader.LoadTutorial();
           
        }
        else
        {
            LevelLoader.leveLoader.LoadNextScene();
        }


    }

    private void Update()
    {
        //CheckAddStatus();

        if (playerLives <= 0)
        {
            playerLives = 0;
           
            
            if (!shouldSetCoolDownTime)
            {
                shouldSetCoolDownTime = true;
                SetCooldDownTime();
            }
            
        }

        OutOfLivesTimer();
    }

    private void OutOfLivesTimer()
    {
        if (playerLives <= 0)
        {
          
            currentTime = System.DateTime.Now; //loggedInTime Startissa
            //SetCooldDownTime();
            
            coolDownLeft = setCoolDownTime - currentTime;
            TimeSpan currentTimespan = System.TimeSpan.Zero;

            int compareResult = coolDownLeft.CompareTo(currentTimespan);
            //Debug.Log(compareResult);
       
            if (compareResult !=1 )
            {
                Debug.Log("CoolDownDone and player rewarded");
                playerLives += amountToRewardAfterTimer;
                timerStarted = false;
                shouldSetCoolDownTime = false;               
                SaveGame();
                SceneManager.LoadScene("Repaired");
            }

        }
    }

    private void SetCooldDownTime()
    {
        setCoolDownTime = System.DateTime.Now.AddMinutes(coolDownTimeAmount); //Tässä asetetaan cooldownin kesto.
        //Debug.Log("Cooldown set" + setCoolDownTime);
        //Debug.Log("Timer started");
        SaveGame();
        timerStarted = true;
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void RepairButton()
    {
        if (totalCoins >= 1000)
        {
            totalCoins -= 1000;
            playerLives += amountIfPayedRepair;
            gameManager.shouldSetCoolDownTime = false;
            gameManager.timerStarted = false;
            SaveGame();
            SceneManager.LoadScene("Repaired");
            Debug.Log("RepairButtonPressed");
        }
        else
        {
            return;
        }
    }

    /*public void CheckAddStatus()
    {
        if(roundsOfGamePlayed >= roundsToPlayBeforeadds)
        {
            roundsOfGamePlayed = 0;
        }

    }
    */

    public int GetTotalCoinAmount()
    {
        return totalCoins;
    }
    public float GetBestPlayerDistance()
    {
        return bestPlayerDistance;
    }

    public int GetPlayerLives()
    {
        return playerLives;
    }

    //Achievements

    public void AddCoinsAllTime()
    {
        coinsCollectedAlltime++;
        if(coinsCollectedAlltime == 1)
        {
            Social.ReportProgress(GPGSIds.achievement_one_for_the_piggy_bank, 100.0f, (bool success) => {
                // handle success or failure
            });

        }
        if(coinsCollectedAlltime == 50)
        {
            Social.ReportProgress(GPGSIds.achievement_coin_collector, 100.0f, (bool success) => {
                // handle success or failure
            });
        }
        if(coinsCollectedAlltime == 100)
        {
            Social.ReportProgress(GPGSIds.achievement_saving_for_retirement, 100.0f, (bool success) => {
                // handle success or failure
            });
        }
        if(coinsCollectedAlltime == 1000)
        {
            Social.ReportProgress(GPGSIds.achievement_the_bank, 100.0f, (bool success) => {
                // handle success or failure
            });
        }

    }

    public void DistanceTravelledAchievement()
    {
        distanceTravelled = FindObjectOfType<PlayerDistance>().GetDistanceAmount();
        if(distanceTravelled > 500 && distanceTravelled < 1000)
        {
            Social.ReportProgress(GPGSIds.achievement_learning_to_fly, 100.0f, (bool success) => {
                // handle success or failure
            });
        }
        if(distanceTravelled > 1000 && distanceTravelled < 3000)
        {
            Social.ReportProgress(GPGSIds.achievement_aces_high, 100.0f, (bool success) => {
                // handle success or failure
            });
        }
        if(distanceTravelled > 3000 && distanceTravelled < 5000)
        {
            Social.ReportProgress(GPGSIds.achievement_flight_of_the_icarus, 100.0f, (bool success) => {
                // handle success or failure
            });
        }
        if(distanceTravelled > 5000)
        {
            Social.ReportProgress(GPGSIds.achievement_to_the_moon, 100.0f, (bool success) => {
                // handle success or failure
            });
        }
    }

    public void DiedFromCannon()
    {
        // increment achievement (achievement ID "Cfjewijawiu_QA") by 5 steps
        PlayGamesPlatform.Instance.IncrementAchievement(
            getRektAchi, 1, (bool success) => {
            // handle success or failure
        });

        timesDiedFromCannon++;
        if(timesDiedFromCannon == 100)
        {
          //add achi
        }
    }

    public void DiedFromCrash()
    {
        // increment achievement (achievement ID "Cfjewijawiu_QA") by 5 steps
        PlayGamesPlatform.Instance.IncrementAchievement(
            kamikazeAchi, 1, (bool success) => {
                // handle success or failure
            });

        timesDiedFromCrash++;
        if(timesDiedFromCrash == 100)
        {
            //Kamikaze achi
        }
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("reset");

        bestPlayerDistance = 0;
        totalCoins = 0;
        coinsCollectedAlltime = 0;
        timesDiedFromCannon = 0;
        timesDiedFromCrash = 0;
        SaveGame();
    }

    public void SetBestPlayerDistance(float BestDistanceEver)
    {
        bestPlayerDistance = BestDistanceEver;
    }

    public void SaveGame()
    {
        Debug.Log("Game Saved");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        data.timesDiedFromCannon = timesDiedFromCannon;
        data.timesDiedFromCrash = timesDiedFromCrash;
        data.coinsCollectedAlltime = coinsCollectedAlltime;
        data.totalCoins = totalCoins;
        data.bestPlayerDistance = bestPlayerDistance;
        data.playerLives = playerLives;
        data.setCoolDownTime = setCoolDownTime;
        data.shouldSetCoolDownTime = shouldSetCoolDownTime;
        data.isTutorialWatched = isTutorialWatched;

        bf.Serialize(file, data);
        file.Close();

    }

    public void LoadSavedGame()
    {
        Debug.Log("Game Loaded");
        //AINA Loadin alussa, tsekkaa että on jotain mitä ladata.
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            timesDiedFromCannon = data.timesDiedFromCannon;
            timesDiedFromCrash = data.timesDiedFromCrash;
            coinsCollectedAlltime = data.coinsCollectedAlltime;
            totalCoins = data.totalCoins;
            bestPlayerDistance = data.bestPlayerDistance;
            playerLives = data.playerLives;
            setCoolDownTime = data.setCoolDownTime;
            shouldSetCoolDownTime = data.shouldSetCoolDownTime;
            isTutorialWatched = data.isTutorialWatched;
           
        }

    }



}

[Serializable]
class PlayerData
{
    public int timesDiedFromCannon;
    public int timesDiedFromCrash;
    public int totalCoins;
    public int coinsCollectedAlltime;
    public int playerLives;
    public float bestPlayerDistance;
    public DateTime setCoolDownTime;
    public bool shouldSetCoolDownTime;
    public bool isTutorialWatched;

}

//public float maxHealth;

//public string currentLevel;
//public bool Level1;
//public bool Level2;
//public bool Level3;



