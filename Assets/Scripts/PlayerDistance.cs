using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDistance : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] public float playerDistance;
    [SerializeField] public float bestPlayerDistance;
    [SerializeField] TextMeshProUGUI bestPlayerDistanceText;
    [SerializeField] TextMeshProUGUI bestDistance;
    [SerializeField] GameObject playerGO;
    float offSet = -8.1f;

    public bool isCountingDistance = false;



    private void Start()
    {
        bestPlayerDistance = GameManager.gameManager.GetBestPlayerDistance();
        bestDistance.text = bestPlayerDistance.ToString("0");
    }

    void Update()
    {
        CountDistance();
    }

    private void CountDistance()
    {

        if (FindObjectOfType<SessionManager>().gameHasStarted && isCountingDistance == true)
        {
            if (FindObjectOfType<PlaneControl>().isDead == false)
            {
                playerDistance = ((playerGO.transform.position.x + offSet) * 10);
                distanceText.text = playerDistance.ToString("0");

                if(playerDistance > 500 && playerDistance < 501 || playerDistance > 1000 && playerDistance < 1001 || playerDistance > 3000 && playerDistance < 3001 || playerDistance > 5000 && playerDistance < 5001)
                {
                    GameManager.gameManager.DistanceTravelledAchievement();
                    Debug.Log("DistanceAchi" + playerDistance);
                }

            }
            else
            {
                return;
            }
            SetBestDistance();

        }
    }

    public void SetBestDistance()
    {
        if (playerDistance > bestPlayerDistance)
        {
            bestPlayerDistance = playerDistance;
            bestDistance.text = playerDistance.ToString("0");
            GameManager.gameManager.SetBestPlayerDistance(playerDistance);
        }
        else
        {
            bestPlayerDistance = GameManager.gameManager.GetBestPlayerDistance();
            bestPlayerDistanceText.text = bestPlayerDistance.ToString("0");
        }
    }

    public float GetDistanceAmount()
    {
        return playerDistance;
    }

    public float GetBestPlayerDistance()
    {
        return bestPlayerDistance;
    }
    
}



