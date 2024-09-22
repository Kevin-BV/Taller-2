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
    private Vector3 moveDirection; // Vector para el movimiento

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Obtener el componente Animator del personaje
    }

    void Update()
    {
        // Movimiento lateral (izquierda/derecha)
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        // Movimiento en profundidad (adelante/atrás)
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;

        // Aplicar movimiento
        rb.velocity = new Vector3(moveX, rb.velocity.y, moveZ);

        // Verificar si el personaje está en el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Saltar si el personaje está en el suelo y se presiona la tecla de salto
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Solo rotar en función del movimiento lateral (izquierda/derecha)
        if (moveX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0); // Gira hacia la derecha
        }
        else if (moveX < 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0); // Gira hacia la izquierda
        }

        // Actualizar el parámetro "Speed" en el Animator
        float speed = new Vector3(moveX, 0, moveZ).magnitude; // Magnitud del movimiento
        animator.SetFloat("Speed", speed); // Establecer el parámetro Speed
    }

    void OnDrawGizmos()
    {
        // Dibujar la esfera para verificar si está en el suelo (solo para ver en el editor)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}