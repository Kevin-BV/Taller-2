using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator animator; // Referencia al Animator del enemigo
    public Transform player; // Referencia al jugador
    public float detectionRange = 5f; // Rango de detección del jugador
    public float attackRange = 1.5f; // Rango de ataque
    public float moveSpeed = 2f; // Velocidad de movimiento del enemigo
    public float attackCooldown = 3f; // Tiempo de espera entre ataques
    public int attackDamage = 10; // Daño que inflige el enemigo
    public float stopDuration = 1f; // Duración de la pausa después de atacar (editable en Inspector)

    private bool playerInRange = false;
    private float nextAttackTime = 0f; // Control del tiempo entre ataques
    private bool isAttacking = false;

    void Update()
    {
        // Calcular la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Verificar si el jugador está dentro del rango de detección
        if (distanceToPlayer <= detectionRange)
        {
            playerInRange = true;

            // Si no está atacando, seguir al jugador
            if (!isAttacking)
            {
                FollowPlayer();
            }
        }
        else
        {
            playerInRange = false;
            StopWalking();
        }

        // Si el jugador está en rango de ataque y ha pasado el tiempo de cooldown, atacar
        if (playerInRange && distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown; // Reiniciar el cooldown
        }
    }

    void FollowPlayer()
    {
        // Obtener la dirección hacia el jugador en el eje X
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Ignorar cualquier diferencia en el eje Y

        // Mover al enemigo hacia el jugador en el eje X solamente
        if (direction.x != 0)
        {
            // Solo rotar en el eje Y para mirar a la izquierda o derecha
            float lookDirection = direction.x > 0 ? 90f : -90f; // Mirar hacia la derecha (90) o izquierda (-90)
            transform.rotation = Quaternion.Euler(0, lookDirection, 0); // Solo afecta el eje Y

            // Mover hacia el jugador en el eje X sin cambiar el Y
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);

            // Activar la animación de caminar
            StartWalking();
        }
        else
        {
            StopWalking();
        }
    }

    void Attack()
    {
        // Detener el movimiento durante el ataque
        StopWalking();

        // Iniciar la animación de ataque
        animator.SetTrigger("Punching");

        isAttacking = true;
        Invoke("ResumeMovement", stopDuration); // Reanudar el movimiento después de `stopDuration`

        // Aplicar daño al jugador (aquí debes tener un script que controle la salud del jugador)
        if (player.GetComponent<PlayerHealth>() != null)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage); // Suponiendo que el jugador tiene un método TakeDamage()
        }
    }

    void ResumeMovement()
    {
        // Reanudar el movimiento del enemigo
        isAttacking = false;
    }

    void StartWalking()
    {
        animator.SetBool("isWalking", true); // Activar la animación de caminar
    }

    void StopWalking()
    {
        animator.SetBool("isWalking", false); // Desactivar la animación de caminar
    }

    // Opcional: Mostrar el rango de detección y ataque en la escena para visualización
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Rango de detección
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Rango de ataque
    }
}