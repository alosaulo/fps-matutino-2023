using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public AudioClip[] sonsZumbi;

    public AudioClip[] sonsDano;

    public AudioClip[] sonsAtaque;

    public AudioSource audioSourceZumbi;

    public float velocidade;

    public int vida;

    bool morto = false;

    bool tomouDano = false;

    Animator animator;

    GameObject target;

    Rigidbody rigidBody;

    public GameObject ZombieAtk;

    SpawnController spawnController;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody>();
        spawnController = GameObject.
            FindGameObjectWithTag("GameController").GetComponent<SpawnController>();
        audioSourceZumbi = GetComponent<AudioSource>();
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
            TocarSomAtaque();
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
        TocarSomDano();
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
        TocarSomDano();

        gameManager.AtualizarScore();

        animator.SetBool("die", true);
        morto = true;

        rigidBody.useGravity = false;
        rigidBody.velocity = Vector3.zero;
        
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        spawnController.MeRemove(gameObject);
    }

    public void SetSpawnController(SpawnController controller) 
    {
        this.spawnController = controller;
    }

    public void AtivarAtk() 
    {
        ZombieAtk.SetActive(true);
    }

    public void DesativarAtk() 
    {
        ZombieAtk.SetActive(false);
    }

    void TocarSomZumbi() 
    {
        if (!audioSourceZumbi.isPlaying) 
        { 
            int aleatorio = Random.Range(0, sonsZumbi.Length);
            audioSourceZumbi.PlayOneShot(sonsZumbi[aleatorio]);
        }
    }

    void TocarSomAtaque() 
    {
        if (!audioSourceZumbi.isPlaying)
        {
            int aleatorio = Random.Range(0, sonsAtaque.Length);
            audioSourceZumbi.PlayOneShot(sonsAtaque[aleatorio]);
        }
    }

    void TocarSomDano() 
    {
        if (!audioSourceZumbi.isPlaying)
        {
            int aleatorio = Random.Range(0, sonsDano.Length);
            audioSourceZumbi.PlayOneShot(sonsDano[aleatorio]);
        }
    }

}
