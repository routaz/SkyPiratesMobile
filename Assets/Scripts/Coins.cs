using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] int coinValue;
    [SerializeField] GameObject collectVFX;
    private GameObject coinsVFX;
    // Start is called before the first frame update
    public void CollectCoins(Collider2D Coins)
    {
        FindObjectOfType<CoinCollector>().CollectCoin(coinValue);
        coinsVFX = Instantiate(collectVFX, Coins.transform.position, Quaternion.identity);
        Destroy(coinsVFX, 2);
        
        Coins.gameObject.SetActive(false);
        Destroy(Coins.gameObject);
    }
}
