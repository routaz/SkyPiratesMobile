using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelConsumption : MonoBehaviour
{
    public float maxFuel = 100;
    public float fuel;
    public float fuelConsumption = 1;
    private float addFuelAmount;

    // Start is called before the first frame update
    void Start()
    {
        fuel = maxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && FindObjectOfType<PlaneControl>().engineIsOn)
        {
            fuel -= Time.deltaTime * fuelConsumption;
            if(fuel <= 0)
            {
                fuel = 0;
            }
            if(fuel >=100)
            {
                fuel = 100;
            }

        }
        
    }

    public void CollectFuel(Collider2D FuelCan)
    {

        fuel += addFuelAmount;
        FuelCan.gameObject.SetActive(false);
        Destroy(FuelCan.gameObject);
    }
}
