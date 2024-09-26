using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public float jumpForce = 7f; // Fuerza del salto
    public LayerMask groundLayer; // Capa del suelo para verificar si el personaje est� en el suelo
    public Transform groundCheck; // Objeto que verifica si el personaje est� en el suelo
    public float groundCheckRadius = 0.2f; // Radio para verificar si el personaje est� en el suelo
    public Animator animator; // Referencia al Animator

    private Rigidbody rb;
    private bool isGrounded;
    private bool isJumping; // Para controlar el estado de salto

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Obtener el componente Animator del personaje
    }

    void Update()
    {
        // Comprobar si hay enemigos en la escena
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            animator.SetTrigger("Dancing"); // Activar animaci�n de Dancing si no hay enemigos
            return; // Salir del Update si no hay enemigos
        }

        // Movimiento lateral (izquierda/derecha) en 3D
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        // Movimiento en profundidad (adelante/atr�s) en 3D
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;

        // Aplicar movimiento en 3D
        rb.velocity = new Vector3(moveX, rb.velocity.y, moveZ);

        // Verificar si el personaje est� en el suelo usando una esfera debajo del personaje
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Control de la animaci�n de salto
        if (isGrounded) // Si el personaje est� en el suelo
        {
            animator.SetBool("IsJumping", false); // Desactiva la animaci�n de salto
            float speed = new Vector3(moveX, 0, moveZ).magnitude;
            animator.SetFloat("Speed", speed); // Actualiza las animaciones de Idle o Walk
        }

        // Saltar si el personaje est� en el suelo y se presiona la tecla de salto
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("IsJumping", true); // Activa la animaci�n de salto
        }

        // Detectar cuando el personaje aterriza
        if (!isGrounded && rb.velocity.y < 0) // Si est� cayendo
        {
            animator.SetBool("IsJumping", true); // Mantiene la animaci�n de salto activa hasta aterrizar
        }

        // Rotaci�n solo cuando el personaje se mueve a los lados
        if (moveX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0); // Gira hacia la derecha (en 3D)
        }
        else if (moveX < 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0); // Gira hacia la izquierda (en 3D)
        }
    }

    void OnDrawGizmos()
    {
        // Dibujar la esfera para verificar si est� en el suelo (solo para ver en el editor)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}