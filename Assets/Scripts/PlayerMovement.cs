using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public float jumpForce = 7f; // Fuerza del salto
    public LayerMask groundLayer; // Capa del suelo para verificar si el personaje está en el suelo
    public Transform groundCheck; // Objeto que verifica si el personaje está en el suelo
    public float groundCheckRadius = 0.2f; // Radio para verificar si el personaje está en el suelo
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
        // Movimiento lateral (izquierda/derecha) en 3D
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        // Movimiento en profundidad (adelante/atrás) en 3D
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;

        // Aplicar movimiento en 3D
        rb.velocity = new Vector3(moveX, rb.velocity.y, moveZ);

        // Verificar si el personaje está en el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Control de la animación de salto
        if (isGrounded && !isJumping) // Si el personaje está en el suelo y no está saltando
        {
            animator.SetBool("IsJumping", false); // Desactiva la animación de salto
            float speed = new Vector3(moveX, 0, moveZ).magnitude;
            animator.SetFloat("Speed", speed); // Mantiene las animaciones de Idle o Walk
        }

        // Saltar si el personaje está en el suelo y se presiona la tecla de salto
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true; // Ahora el personaje está en el aire
            animator.SetBool("IsJumping", true); // Activa la animación de salto
        }

        // Detectar cuando el personaje aterriza
        if (isJumping && isGrounded)
        {
            isJumping = false; // El personaje ya no está en el aire
            animator.SetBool("IsJumping", false); // Desactiva la animación de salto
        }

        // Rotación solo cuando el personaje se mueve a los lados
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
        // Dibujar la esfera para verificar si está en el suelo (solo para ver en el editor)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}