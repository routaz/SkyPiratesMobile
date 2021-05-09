using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControl : MonoBehaviour
{
    [Header("ShootAttributes")]
    [SerializeField] float shootSpeed = 10 ;
    public float fireRate = 1f;

    [SerializeField] float fireCoolMin = 1f;
    [SerializeField] float fireCoolMax = 10f;
    [SerializeField] float fireCooldown;

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject projectile;
    [SerializeField] ParticleSystem cannonSmoke;
 
    [Header("CannonSetup")]
    public Transform cBase;
    private Transform target;

    public float range = 1f;
    public float rotationSpeed;

    private Vector3 clampedRotation;
    private GameObject playerGo;

    AudioSource audioSource;
    [SerializeField] AudioClip[] cannonBlasts;

    private void Start()
    {
        if (FindObjectOfType<SessionManager>().gameHasStarted && FindObjectOfType<PlaneControl>().isDead == false)
        {
            playerGo = GameObject.FindGameObjectWithTag("Player");
            target = playerGo.transform;
        }

        audioSource = GetComponent<AudioSource>();

    }
    void Update()
    {
        if (target == null || playerGo == null)
            return;

        RotateCannon();


        fireRate += Time.deltaTime;

        fireCooldown = Random.Range(fireCoolMin, fireCoolMax);
        if (fireRate >= fireCooldown)
        {
            Debug.Log("Shoot");
            Shoot();
            fireRate = 0;
        }

    }

    private void RotateCannon()
    {
        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.x, -dir.y) * Mathf.Rad2Deg;
        clampedRotation.z = Mathf.Clamp(angle, -113f, 113f);
        Quaternion rotation = Quaternion.Euler(0f, 0f, clampedRotation.z);
        cBase.rotation = Quaternion.Slerp(cBase.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        if (target.position.x > firePoint.position.x)
        {
            Debug.Log("Ei ammu");
            return;

        }
        else
        {
            GameObject cannonBall = Instantiate(projectile, firePoint.transform.position, Quaternion.identity);
            cannonBall.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.up * shootSpeed * transform.localScale.x, ForceMode2D.Impulse);
            Instantiate(cannonSmoke, firePoint.transform.position, Quaternion.identity);
            var blast = cannonBlasts[Random.Range(0, cannonBlasts.Length)];
            audioSource.PlayOneShot(blast);
        }

    }



}


