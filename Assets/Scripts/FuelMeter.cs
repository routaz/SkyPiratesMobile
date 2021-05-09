using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelMeter : MonoBehaviour
{
    float currentFuel;
    //float max = 100;
    //float min = 0;

    [SerializeField] float counter;
    [SerializeField]float maxCounter;
    float previousFuel;
   
    public Image mask;

    public void Start()
    {
     
    }
    public void Update()
    {
        if (FindObjectOfType<SessionManager>().gameHasStarted)
        {
            if (FindObjectOfType<PlaneControl>().isDead == false)
            {
                currentFuel = FindObjectOfType<FuelConsumption>().fuel;
                GetCurrentFill();
            }
            else
            {
                return;
            }
        }
        
    }
    void GetCurrentFill()
    {
        if (counter > maxCounter)
        {
            previousFuel = currentFuel;
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }

        //float currentOffset = currentFuel - min;
        //float maxOffset = max - min;
        //float fillAmount = currentOffset / maxOffset;
        //mask.fillAmount = fillAmount;
        mask.fillAmount = Mathf.Lerp(previousFuel / FindObjectOfType<FuelConsumption>().maxFuel, FindObjectOfType<FuelConsumption>().fuel / FindObjectOfType<FuelConsumption>().maxFuel, counter / maxCounter);

    }

}

