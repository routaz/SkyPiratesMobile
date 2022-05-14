using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using TMPro;

public class GPGSManager : MonoBehaviour
{
    private PlayGamesClientConfiguration clientConfiguration;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI descriptionText;


    // Start is called before the first frame update
    void Start()
    {
        ConfigureGPGS();
        SignIntoGPGS(SignInInteractivity.CanPromptOnce, clientConfiguration);
    }

    internal void ConfigureGPGS()
    {
        clientConfiguration = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();

    }

    internal void SignIntoGPGS(SignInInteractivity interactivity, PlayGamesClientConfiguration configuration)
    {
        configuration = clientConfiguration;
        PlayGamesPlatform.InitializeInstance(configuration);
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(interactivity, (code) => 
            {
            statusText.text = "Authenticating...";
            if(code == SignInStatus.Success)
                {
                    statusText.text = "Succesfully Authenticated";
                    descriptionText.text = "Hello " + Social.localUser.userName + " You have ID of " + Social.localUser.id;
                }
                else
                {
                    statusText.text = "Failed to Authenticate";
                    descriptionText.text = "Faield to Authenticate, reason for failure is " + code;

                }
        });

    }

    public void BasicSignInButton()
    {
        SignIntoGPGS(SignInInteractivity.CanPromptAlways, clientConfiguration);
    }

    public void SignOutButton()
    {
        PlayGamesPlatform.Instance.SignOut();
        statusText.text = "Ssigned out";
        descriptionText.text = "";
    }
}
