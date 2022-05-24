using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using TMPro;

public class Achievements : MonoBehaviour
{
    public TextMeshProUGUI logText;

    public void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();
    }

    public void DoGrantAchievement(string _achievement)
    {
        Social.ReportProgress(_achievement,
            100.00f,
            (bool success) =>
            {
                if (success) //if success is true
                {
                    logText.text = _achievement + " : " + success.ToString();
                    //Perform new actions here on success
                }
                else
                {
                    logText.text = _achievement + " : " + success.ToString();
                    //Perform new actions here on failure
                }

            }
            );
    }

    public void DoIncrementalAchievemtn(string _achievement)
    {
        PlayGamesPlatform platform = (PlayGamesPlatform)Social.Active;

        platform.IncrementAchievement(_achievement,
            1,
            (bool success) =>
            {
                if (success) //if success is true
                {
                    logText.text = _achievement + " : " + success.ToString();
                    //Perform new actions here on success
                }
                else
                {
                    logText.text = _achievement + " : " + success.ToString();
                    //Perform new actions here on failure
                }

            }
            );

    }

    public void DoRevealAchievement(string _achievement)
    {
        Social.ReportProgress(_achievement,
            0.00f,
            (bool success) =>
            {
                if (success) //if success is true
                {
                    logText.text = _achievement + " : " + success.ToString();
                    //Perform new actions here on success
                }
                else
                {
                    logText.text = _achievement + " : " + success.ToString();
                    //Perform new actions here on failure
                }

            }
            );
    }

    public void ListAchievemnts()
    {
        Social.LoadAchievements(achievements =>
        {
            logText.text = "Loaded Achievements" + achievements.Length;
            foreach (IAchievement ach in achievements)
            {
                logText.text += "/n" + ach.id + " " + ach.completed;
            }

        });
    }

    public void ListDescriptions()
    {
        Social.LoadAchievementDescriptions(achievements =>
        {
            logText.text = "Loaded Achievements" + achievements.Length;
            foreach (IAchievementDescription ach in achievements)
            {
                logText.text += "/n" + ach.id + " " + ach.title;
            }

        });

    }

    public void GrantAchievementBtn()
    {
        DoGrantAchievement(GPGSIds.achievement_unlock_achievement);
    }

    public void GrantIncrementalBtn()
    {
        DoIncrementalAchievemtn(GPGSIds.achievement_incremental);
    }

    public void RevealAchievementBtn()
    {
        DoRevealAchievement(GPGSIds.achievement_hidden_unlock_ach);
    }

    public void RevealIncrementalBtn()
    {
        DoRevealAchievement(GPGSIds.achievement_hidden_incremental);

    }

    public void GrantHiddenAchievemntBtn()
    {
        DoGrantAchievement(GPGSIds.achievement_hidden_unlock_ach);
    }

    public void GrantHiddenIncrementalAchievement()
    {
        DoIncrementalAchievemtn(GPGSIds.achievement_hidden_incremental);
    }



}
