using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer audioPlayer;
    public AudioSource audioSource;

    [SerializeField] AudioClip[] pirateMusic;
    
    public string currentSceneName;
    public bool pirateMusicIsOn = false;

    void Awake()
    {
        if (audioPlayer== null)
        {
            DontDestroyOnLoad(gameObject);
            audioPlayer = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.volume = PlayerPrefsController.GetMasterVolume();
        //PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (currentSceneName != "TheGame" && !audioSource.isPlaying || currentSceneName != "TheGame" && pirateMusicIsOn)
        {
            Debug.Log("ShouldPlayMenuMusic");
            audioSource.clip = pirateMusic[0];
            audioSource.Play();
            pirateMusicIsOn = false;

        }
        else if (currentSceneName == "TheGame" && !pirateMusicIsOn || currentSceneName == "TheGame" && !audioSource.isPlaying)
        {
            Debug.Log("ShouldPlayGameMusic");
            audioSource.clip = pirateMusic[1];
            audioSource.PlayDelayed(4);
            pirateMusicIsOn = true;
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}

