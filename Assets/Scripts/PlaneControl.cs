using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlaneControl : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip crashSFX;

    SpriteRenderer spriteRenderer;
    Rigidbody2D player_rb;
    Quaternion forwardRotation;
    Quaternion downwardRotation;

    [SerializeField] float tapForce;
    [SerializeField] float tiltSmooth;

    [SerializeField] AudioClip collectGasSound;
    [SerializeField] AudioClip planeEngineIdleSound;

    [SerializeField] int startingPitch = 1;
    [SerializeField] int timeToDecrease = 5;

    [SerializeField] Transform engineSmokeSpawn;
    [SerializeField] ParticleSystem engineSmoke;
    [SerializeField] GameObject explosion;

    //[SerializeField] float dashCoolddown = 0.2f;
    [SerializeField] float dashTimer;
    [SerializeField] int buttonPressCount;

    Animator anim;
   
    //public Text coinsText;

    public bool isDead = false;
    public bool engineIsOn = false;

    void Start()
    {
        isDead = false;

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.pitch = startingPitch;

        player_rb = GetComponent<Rigidbody2D>();
        forwardRotation = Quaternion.Euler(0, 0, 35);
        downwardRotation = Quaternion.Euler(0, 0, -60);
        spriteRenderer = GetComponent<SpriteRenderer>();
 
        FindObjectOfType<PlayerDistance>().isCountingDistance = true;
        engineSmoke = Instantiate(engineSmoke, engineSmokeSpawn.transform.position, Quaternion.Euler(0,-90,0));
        engineSmoke.transform.parent = engineSmokeSpawn.transform.parent;

        anim = transform.parent.parent.GetComponent<Animator>();

    }
    // Update is called once per frame
    public void Update()
    {

        if (FindObjectOfType<SessionManager>().gameHasEnded)
        {
            isDead = true;
        }
        Fly();
        EngineSound();

        dashTimer += Time.deltaTime;

        //Dash();

    }

    /*private void Dash()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (dashTimer > dashCoolddown)
            {
                Debug.Log("DashCoolDownLimit");
                dashTimer = 0;
                buttonPressCount = 0;
            }
            else
            {
                buttonPressCount += 1;
            }

            if (buttonPressCount == 1 && Input.GetMouseButtonDown(0))
            {
                Debug.Log("dash Performed");
                buttonPressCount = 0;

                anim.SetTrigger("Dash");
            }

        }
    }
    */

    private void FixedUpdate()
    {
        if (engineIsOn)
        {
            player_rb.velocity = new Vector3(0f,0f,0f);
            player_rb.AddForce(Vector2.up * tapForce, ForceMode2D.Impulse);
            transform.rotation = forwardRotation;
            audioSource.pitch += Time.deltaTime * 2;
            engineSmoke.Play();
            
        }

        else if(!engineIsOn)
        {
            player_rb.AddForce(new Vector2(0f,0f), ForceMode2D.Impulse);
            transform.rotation = Quaternion.Lerp(transform.rotation, downwardRotation, tiltSmooth * Time.deltaTime);
            engineSmoke.Stop();
        }

        if(isDead)
        {
            player_rb.AddForce(new Vector2(0f, 0f), ForceMode2D.Impulse);
            
        }
    }


    public void Fly()
    {
        

        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            engineIsOn = true;
            
        }

        if (Input.GetMouseButtonUp(0) || isDead)
        {
            engineIsOn = false;
        }

        float fuel = FindObjectOfType<FuelConsumption>().fuel;
        if(fuel <= 0)
        {
            engineIsOn = false;
        }

    }

    public void OnTriggerEnter2D(Collider2D col)
    {

        
            if (col.gameObject.CompareTag("Coins"))
            {
            GameManager.gameManager.AddCoinsAllTime();
            {
                FindObjectOfType<Coins>().CollectCoins(col);
                }
            }
        

        if (col.gameObject.CompareTag("Pirate_Balloon"))
        {
            GameManager.gameManager.DiedFromCrash();
            HitByLaser();
        }
 

        if (col.gameObject.CompareTag("EndGame"))
        {

            FindObjectOfType<SessionManager>().EndGame();

        }

        if (col.gameObject.CompareTag("FuelCan"))
        {
            {
                audioSource.PlayOneShot(collectGasSound);
                FindObjectOfType<FuelCan>().CollectFuel(col);

            }
        }

        if(col.gameObject.CompareTag("CannonBall"))
        {
            GameManager.gameManager.DiedFromCannon();
            HitByLaser();
        }


    }
    
    public void HitByLaser()
    {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.PlayOneShot(crashSFX);

        FindObjectOfType<PlayerDistance>().isCountingDistance = false;
        explosion = Instantiate(explosion, player_rb.transform.position, Quaternion.identity);
        Destroy(explosion, 5);
        isDead = true;
        spriteRenderer.enabled = false;
        //Destroy(gameObject);
        GetComponent<CapsuleCollider2D>().enabled = false;
        FindObjectOfType<SessionManager>().EndGame();

    }

    public void EngineSound()
    {
        if (audioSource.pitch > startingPitch)
        {
            audioSource.pitch -= Time.deltaTime * startingPitch / timeToDecrease;
        }
        if(isDead)
        {
            audioSource.Pause();
        }

    }

}

