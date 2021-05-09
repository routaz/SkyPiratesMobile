using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCan : MonoBehaviour
{

    public float addFuelAmount;
    // Start is called before the first frame update
  
    public void CollectFuel(Collider2D FuelCan)
    {

        FindObjectOfType<FuelConsumption>().fuel += addFuelAmount;
        Debug.Log("PlayGasSound");
        FuelCan.gameObject.SetActive(false);
        Destroy(FuelCan.gameObject);
    }


}
