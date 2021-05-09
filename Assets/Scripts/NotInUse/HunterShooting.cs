using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterShooting : MonoBehaviour
{
    public GameObject target;
    public GameObject firePoint;
    public Rigidbody2D rb;

    private float shootTimer;
    private float lastShotTime;
    public float speed = 1f;

    private bool alreadyShot;


    // Start is called before the first frame update

   
    private void Start()
    {
        StartCoroutine(FindTarget());
        lastShotTime = 0f;
        alreadyShot = false;
    }

    // Update is called once per frame
    private void Update()
    {


        float targetErrorX = Random.Range(-1f, 1f);
        float targetErrorY = Random.Range(-1f, 1f);

        Vector3 myTarget = target.transform.position;
        myTarget = new Vector3(myTarget.x + targetErrorX, myTarget.y + targetErrorY, myTarget.z);

        Vector3 difference = myTarget - firePoint.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        firePoint.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 20 && alreadyShot == false)
        {
            alreadyShot = true;
            ShootWeapon();
        }

        if (alreadyShot == true)
        {
            float checkTime = lastShotTime;
            checkTime -= Time.time;
            checkTime = -checkTime;

            if (checkTime > 1.5f)
            {
                alreadyShot = false;
                lastShotTime = 0;
            }
        }
    }

    private void ShootWeapon()
    {
        GameObject obj = AlienAmmoPooler.SharedInstance.GetPooledObject();
        if (obj == null) return;
        obj.transform.position = firePoint.transform.position;
        obj.transform.rotation = firePoint.transform.rotation;
        rb = gameObject.GetComponent<Rigidbody2D>();

         if (rb != null)
        {
            rb.velocity = transform.right * speed;
        }

        Invoke("Disable", 2f);


        obj.SetActive(true);
        lastShotTime = Time.time;
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }



    IEnumerator FindTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        yield return new WaitForSeconds(5);
        StartCoroutine(FindTarget());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Alien ship hit: " + collision.transform.name);
        if (collision.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.GetComponent<CircleCollider2D>(), gameObject.GetComponent<BoxCollider2D>());
        }
    }
}
