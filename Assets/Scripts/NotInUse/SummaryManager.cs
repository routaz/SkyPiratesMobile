using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SummaryManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalCoinsText;
    [SerializeField] int totalCoins;
    [SerializeField] TextMeshProUGUI currentFlightText;
    [SerializeField] float currentFlight;
    [SerializeField] TextMeshProUGUI bestDistanceText;
    [SerializeField] float bestDistance;
    [SerializeField] int coinsCollected;
    [SerializeField] TextMeshProUGUI coinsCollectedText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        totalCoins = GameManager.gameManager.GetTotalCoinAmount();
        bestDistance = GameManager.gameManager.GetBestPlayerDistance();

        totalCoinsText.text = totalCoins.ToString("0");
        bestDistanceText.text = bestDistance.ToString("0");

        coinsCollected = FindObjectOfType<CoinCollector>().GetCoinsAmount();
        coinsCollectedText.text = coinsCollected.ToString("0");

        currentFlight = FindObjectOfType<PlayerDistance>().GetDistanceAmount();
        currentFlightText.text = currentFlight.ToString("0");

    }
}
