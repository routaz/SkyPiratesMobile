using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;

public class GooglePlayServicesManager : MonoBehaviour
{
    public static GooglePlayServicesManager googlePlayServicesManager;

    public bool isConnectedToGooglePlayServices;
    
   
    
    private void Awake()
    {
        if (googlePlayServicesManager == null)
        {
            DontDestroyOnLoad(gameObject);
            googlePlayServicesManager = this;
        }
        else
        {
            Destroy(gameObject);
        }


        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    private void Start()
    {
        SignInToGooglePlayServices();
    }

    private void SignInToGooglePlayServices()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
        {
            switch (result)
            {

                case SignInStatus.Success:
                    isConnectedToGooglePlayServices = true;
                    break;

                default:
                    isConnectedToGooglePlayServices = false;
                    break;
            }
        });
        
    }

    public void ShowGoogleLeaderBoard()
    {
        if (isConnectedToGooglePlayServices)
        {
            Social.ShowLeaderboardUI();
        }
        else if (!isConnectedToGooglePlayServices)
        {
            Debug.Log("open");
            LevelLoader.leveLoader.SignIn();
        }

    }
    public void SendDistanceToLeaderboard(float distance)
    {
        if (isConnectedToGooglePlayServices)
        {
            var flownDistance = Convert.ToInt32(distance);
            Social.ReportScore(flownDistance, GPGSIds.leaderboard_best_distance_by_the_scurviest_dogs, (success) =>
           {
               if (!success) Debug.LogError("Unable to post highscore");

           });
        }
        else
        {
            Debug.Log("Not Signed in...unable to report score");
        }
    }

    public void SignInToGoogle()
    {
        Debug.Log("Signed In");
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) =>
        {
            switch (result)
            {
                case SignInStatus.Success:
                    isConnectedToGooglePlayServices = true;
                    LevelLoader.leveLoader.MainMenu();
                    break;

            }
        });
     }

    public void SignOutFromGoogle()
    {
        Debug.Log("Signed Out");
        PlayGamesPlatform.Instance.SignOut();
        isConnectedToGooglePlayServices = false;
        LevelLoader.leveLoader.MainMenu();
    }

}
