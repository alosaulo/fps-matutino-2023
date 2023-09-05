using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Status")]
    public int vida;
    int vidaMax;
    public int municaoMax;
    public int municaoAtual;

    [Header("Física")]
    public bool noChao;
    public float gravidade;
    public float playerSpeed;
    public float forcaPulo;

    Vector3 playerVelocity;
    
    [Header("Controle")]
    public float sensibilidadeMouse;

    [Header("Som")]
    public AudioSource audioSourceArma;

    CharacterController characterController;
    
    Transform cameraTransform;
    float verticalRotation;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        vidaMax = vida;
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        gameManager = FindObjectOfType<GameManager>();
        //gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimento();
        Rotacao();
        Atirar();
        gameManager.AtualizarTMPMunicao(municaoAtual);
    }

    void Movimento()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        noChao = characterController.isGrounded;

        if (noChao && playerVelocity.y < 0) 
        { 
            playerVelocity.y = 0;
        }

        Vector3 move = transform.forward * vAxis + transform.right * hAxis;

        characterController.Move(move * playerSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && noChao) 
        {
            playerVelocity.y += Mathf.Sqrt(forcaPulo * -3.0f * gravidade);
        }

        playerVelocity.y += gravidade * Time.deltaTime;

        characterController.Move(playerVelocity * Time.deltaTime);

    }

    void Rotacao() 
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadeMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadeMouse * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

    }

    void Atirar() 
    {
        if (Input.GetButtonDown("Fire1") && municaoAtual > 0) 
        {
            municaoAtual -= 1;
            audioSourceArma.Play();
            RaycastHit hit;
            Vector3 origemRay = cameraTransform.position;
            Vector3 direcaoRay = cameraTransform.forward;
            float distanciaRay = 10f;

            Debug.DrawRay(origemRay, direcaoRay * distanciaRay, Color.magenta,1f);

            if (Physics.Raycast(origemRay,direcaoRay * distanciaRay,out hit))
            {
                if (hit.transform.tag == "Inimigo") 
                { 
                    ZombieController zombie = 
                        hit.transform.GetComponent<ZombieController>();
                    if (zombie != null)
                    {
                        zombie.TomarDano(1);
                    }
                }
                Debug.Log(hit.transform.name);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "AtkInimigo") 
        {
            TomarDano(1);
        }
    }

    public void TomarDano(int dano) 
    {
        vida -= dano;
        gameManager.AtualizarBarraHP(vida, vidaMax);
        if(vida <= 0) 
        {
            Morrer();
        }
    }

    void Morrer() 
    { 
        gameObject.SetActive(false);
    }

}
