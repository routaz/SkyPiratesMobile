using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
            GameManager.gameManager.shouldSetCoolDownTime = false;
            GameManager.gameManager.timerStarted = false;
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

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("reset");

        bestPlayerDistance = 0;
        totalCoins = 0;
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

    public int totalCoins;
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



