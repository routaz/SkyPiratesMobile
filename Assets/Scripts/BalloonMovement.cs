using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    private float randomDirection;
    [SerializeField] float shiftSpeed;
    [SerializeField] float yPosmin;
    [SerializeField] float yPosmax;

    //AudioSource audioSource;
    //[SerializeField] AudioClip balloonAmbient;

    // Start is called before the first frame update
    void Start()
    {
        randomDirection = Random.Range(yPosmin,yPosmax);
        transform.position += new Vector3(0f, randomDirection, 0f) * Time.deltaTime;


    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position += new Vector3(shiftSpeed, randomDirection, 0f) * Time.deltaTime;
    }
}
