using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCannon : MonoBehaviour
{
    public GameObject bullet;
    public float fireRate;
    public float nextFire;
    public GameObject firePoint;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        CheckIfTimeToFire();
        
    }

    void CheckIfTimeToFire()
    {
        if (nextFire < Time.time)
        {
            GameObject obj = AlienAmmoPooler.SharedInstance.GetPooledObject();
            if (obj == null) return;
            obj.transform.position = firePoint.transform.position;
            obj.transform.rotation = firePoint.transform.rotation;
            obj.SetActive(true);
            nextFire = Time.time + fireRate;
        }

        
    }
}
