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
        if (!isConnectedToGooglePlayServices)
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
        else return;
    }

    public void ShowGoogleLeaderBoard()
    {
        if (!isConnectedToGooglePlayServices)
        {
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
            {
                switch (result)
                {

                    case SignInStatus.Success:
                        isConnectedToGooglePlayServices = true;
                        Social.ShowLeaderboardUI();
                        break;

                    default:
                        isConnectedToGooglePlayServices = false;
                        LevelLoader.leveLoader.SignIn();
                        break;
                }
            });
            
        }
        else
        {
            Social.ShowLeaderboardUI();
        }

    }
    public void SendDistanceToLeaderboard(float distance)
    {
        var flownDistance = Convert.ToInt32(distance);
        if (isConnectedToGooglePlayServices)
        {
            Social.ReportScore(flownDistance, GPGSIds.leaderboard_the_best_distances, (success) =>
           {
               if (!success) Debug.LogError("Unable to post highscore");
               
           });
        }
        else
        {
            Debug.Log("Not Signed in...unable to report score");
            Debug.Log(flownDistance);
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
