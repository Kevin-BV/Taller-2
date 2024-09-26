using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator animator; // Referencia al Animator del enemigo
    public Transform player; // Referencia al jugador
    public float detectionRange = 5f; // Rango de detecci�n del jugador
    public float attackRange = 1.5f; // Rango de ataque
    public float moveSpeed = 2f; // Velocidad de movimiento del enemigo
    public int attackDamage = 10; // Da�o que inflige el enemigo
    public float attackCooldown = 3f; // Tiempo de espera entre ataques

    private bool isAttacking = false; // Estado de ataque

    void Update()
    {
        // Calcular la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Verificar si el jugador est� dentro del rango de detecci�n
        if (distanceToPlayer <= detectionRange)
        {
            // Rotar hacia el jugador
            RotateTowardsPlayer();

            // Si el jugador est� en rango de ataque y no est� atacando, atacar
            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                Attack();
            }
            else if (!isAttacking)
            {
                // Si no est� atacando, avanzar hacia el jugador
                FollowPlayer();
            }
        }
        else
        {
            StopWalking(); // Detener al enemigo si el jugador est� fuera de rango
        }
    }

    void RotateTowardsPlayer()
    {
        // Obtener la direcci�n hacia el jugador en el eje X
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Ignorar cualquier diferencia en el eje Y

        // Solo rotar en el eje Y para mirar a la izquierda o derecha
        if (direction.x != 0)
        {
            float lookDirection = direction.x > 0 ? 90f : -90f; // Mirar hacia la derecha (90) o izquierda (-90)
            transform.rotation = Quaternion.Euler(0, lookDirection, 0); // Solo afecta el eje Y
        }
    }

    void FollowPlayer()
    {
        // Mover al enemigo hacia el jugador en el eje X solamente
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        StartWalking(); // Activar la animaci�n de caminar
    }

    void Attack()
    {
        // Detener el movimiento durante el ataque
        StopWalking();

        // Iniciar la animaci�n de ataque
        animator.SetTrigger("Punching");

        isAttacking = true; // Marcar que est� atacando

        // Aplicar da�o al jugador (asumiendo que el jugador tiene un script que controla su salud)
        if (player.GetComponent<PlayerHealth>() != null)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage); // Suponiendo que el jugador tiene un m�todo TakeDamage()
        }

        // Reiniciar el estado de ataque despu�s de un tiempo de cooldown
        Invoke("ResumeMovement", attackCooldown);
    }

    void ResumeMovement()
    {
        isAttacking = false; // Marcar que ya no est� atacando
    }

    void StartWalking()
    {
        animator.SetBool("isWalking", true); // Activar la animaci�n de caminar
    }

    void StopWalking()
    {
        animator.SetBool("isWalking", false); // Desactivar la animaci�n de caminar
    }

    // Mostrar los gizmos en la escena
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Rango de detecci�n
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Rango de ataque
    }
}