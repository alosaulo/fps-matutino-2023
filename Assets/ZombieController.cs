using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float velocidade;

    public int vida;

    bool morto = false;

    bool tomouDano = false;

    Animator animator;

    GameObject target;

    Rigidbody rigidBody;

    public GameObject ZombieAtk;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(morto == false && tomouDano == false) 
        { 
            DoAI();
            Rotacionar();
        }
    }

    void DoAI() 
    {
        float distancia = Vector3.Distance(transform.position, target.transform.position);
        if (distancia <= 2)//Se a distância for menor ou igual a 2 ele para e ataca
        {
            animator.SetTrigger("atk");
            animator.SetBool("andar", false);
        }
        else //Se a distância for maior que dois ele anda atrás do player
        {
            rigidBody.position = Vector3.MoveTowards
                (rigidBody.position, target.transform.position, velocidade * Time.deltaTime);
            animator.SetBool("andar", true);
        }
    }

    void Rotacionar() 
    {
        Vector3 lookAtPlayer = new Vector3(
            target.transform.position.x,
            transform.position.y,
            target.transform.position.z);

        transform.LookAt(lookAtPlayer, transform.up);
    }

    public void TomarDano(int dano) 
    {
        StartCoroutine("DanoCooldown");
        vida -= dano;
        animator.SetTrigger("dano");
        animator.SetBool("andar", false);
        if (vida <= 0) 
        {
            Morrer();
        }
    }

    IEnumerator DanoCooldown()
    {
        tomouDano = true;
        yield return new WaitForSeconds(1f);
        tomouDano = false;
    }

    void Morrer() 
    {
        animator.SetBool("die", true);
        morto = true;

        rigidBody.useGravity = false;
        rigidBody.velocity = Vector3.zero;
        
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
    }

    public void AtivarAtk() 
    { 
        ZombieAtk.SetActive(true);
    }

    public void DesativarAtk() 
    {
        ZombieAtk.SetActive(false);
    }

}
