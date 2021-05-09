using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{    
    public Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
      
    }
    public void Explode()
    {
        anim.SetTrigger("Explosion");          
    }
}
