using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinMountCheck : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI totalCoinsText;
    [SerializeField] int totalCoins;
    
    // Update is called once per frame
    void Update()
    {
        totalCoins = GameManager.gameManager.GetTotalCoinAmount();
        totalCoinsText.text = totalCoins.ToString("0");
      
    }
}
