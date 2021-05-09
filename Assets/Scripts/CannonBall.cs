using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float shootForce;
    Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * shootForce * transform.localScale.x, ForceMode2D.Impulse);
    }

    public void Update()
    {
        //rb.velocity = transform.right * shootForce;
    }
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<PlaneControl>().HitByLaser();
            Debug.Log("HitThePlayer");
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Boundary"))
        {
            //gameObject.SetActive(false);
        }
    }
}
