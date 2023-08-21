using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public int vida;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TomarDano(int dano) 
    {
        vida -= dano;
        animator.SetTrigger("dano");
        if(vida <= 0) 
        {
            animator.SetBool("die", true);
            //Destroy(gameObject);
        }
    }

}
