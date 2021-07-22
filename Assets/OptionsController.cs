using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] float volumeSliderValue = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = PlayerPrefsController.GetMasterVolume();
    }

    // Update is called once per frame
    void Update()
    {

        var musicPlayer = FindObjectOfType<AudioPlayer>();
        if(musicPlayer)
        {
            musicPlayer.SetVolume(volumeSlider.value);
        }
        else
        {
            Debug.LogWarning("No musicPlayer founnd");
        }
        
    }

    public void SetMasterVolume()
    {
        PlayerPrefsController.SetMasterVolume(volumeSlider.value);
        //FindObjectOfType<LevelLoader>().MainMenu();
    }


    /*public void MuteMusic()
    {
        var musicPlayer = FindObjectOfType<AudioPlayer>();
        if(musicPlayer)
        {
            musicPlayer.SetVolume(0);
        }
        
    }

    public void UnMuteMusic()
    {
        var musicPlayer = FindObjectOfType<AudioPlayer>();
        if(musicPlayer)
        {
            musicPlayer.SetVolume(volumeSlider.value);
        }
    }
    */
}
