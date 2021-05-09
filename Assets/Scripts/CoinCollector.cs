using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public int coins;
    public TextMeshProUGUI totalCoinsText;
    public int totalCoins;
    AudioSource audioSource;
    [SerializeField] AudioClip[] coinCollectSounds;

    public void Start()
    {
        coinsText.text = coins.ToString("0");
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
     
    }

    public void CollectCoin(int coinValue)
    {
        coins += coinValue;
        coinsText.text = coins.ToString();
        SetCoins(coinValue);

        var collectSound = coinCollectSounds[Random.Range(0, coinCollectSounds.Length)];
        audioSource.PlayOneShot(collectSound);
    }

    public void SetCoins(int coinValue)
    {
        GameManager.gameManager.totalCoins += coinValue;
    }

    public int GetCoinsAmount()
    {
        return coins;
    }
/*
    public int GetTotalCoinsAmount()
    {

        return totalCoins;
    }
*/
   
}
