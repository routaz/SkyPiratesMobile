using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class AddManager : MonoBehaviour, IUnityAdsListener
{
    [Header("REWARDS")]
    [SerializeField] int howManyLivesToReward = 5;

    [SerializeField] GameManager gameManager;
    public static AddManager adManager;
    public string gameIDAndroid = "3975829";
    public string gameIDIOS = "3975828";

    public string myVideoPlacement = "rewardedVideo";
    public string myAdStatus = "";
    
   
    public bool adStarted;
    public bool adCompleted;

    private bool testMode = true;
    ShowOptions options = new ShowOptions();

    // Start is called before the first frame update
    void Awake()
    {
        if (adManager == null)
        {
            DontDestroyOnLoad(gameObject);
            adManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void Start()
    {
        
    Advertisement.AddListener(this);
#if UNITY_IOS
        
            Advertisement.Initialize(gameIDIOS, testMode);

#else //#elif UNITY_ANDROID, used if real game.  
        Advertisement.Initialize(gameIDAndroid, testMode);


#endif  
    }

    public void ShowAd(string placement)
    {
        Advertisement.Show(myVideoPlacement);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        adCompleted = showResult == ShowResult.Finished;
        if(showResult == ShowResult.Finished)
        {
            GameManager.gameManager.playerLives += howManyLivesToReward;
            GameManager.gameManager.shouldSetCoolDownTime = false;
            GameManager.gameManager.timerStarted = false;
            LevelLoader.leveLoader.Repaired();
            GameManager.gameManager.SaveGame();
        }
        else if(showResult == ShowResult.Failed)
        {
            return;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        adStarted = true;
    }

    public void OnUnityAdsReady(string placementId)
    {
       

    }

    public void OnUnityAdsDidError(string message)
    {
        myAdStatus = message;
    }
    /*
    public void ShowRewardedVideo()

    {
        StartCoroutine(WaitForAdEditor());
        if (Advertisement.IsReady(myPlacementId))
        {
            Debug.Log("RewardVideoWatchedAndLivesAdded");
            Advertisement.Show(myPlacementId);
        }

        else
        {
            Debug.Log("Reward ad not ready at the moment! Please try again later!");
        }

    }

    private IEnumerator WaitForAdEditor()
    {
        float currentTimescale = Time.timeScale;
        Time.timeScale = 0f;
        AudioListener.pause = true;

        yield return null;

        while (Advertisement.isShowing)
        {
            yield return null;
        }

        AudioListener.pause = false;
        if (currentTimescale > 0f)
        {
            Time.timeScale = currentTimescale;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    */

}
