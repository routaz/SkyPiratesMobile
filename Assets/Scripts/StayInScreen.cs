using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInScreen : MonoBehaviour
{
    [SerializeField] float maxScreenHeight = 5f;
    [SerializeField] float minScreenHeight = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minScreenHeight, maxScreenHeight), transform.position.z);
        
    }
}
