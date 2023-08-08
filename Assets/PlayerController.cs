using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool noChao;
    public float gravidade;
    public float playerSpeed;
    public float forcaPulo;

    Vector3 playerVelocity;

    public float sensibilidadeMouse;

    CharacterController characterController;
    
    Transform cameraTransform;
    float verticalRotation;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Movimento();
        Rotacao();
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

}