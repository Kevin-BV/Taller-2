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
    public float attackCooldown = 3f; // Tiempo de espera entre ataques
    public int attackDamage = 10; // Da�o que inflige el enemigo
    public float stopDuration = 1f; // Duraci�n de la pausa despu�s de atacar (editable en Inspector)

    private bool playerInRange = false;
    private float nextAttackTime = 0f; // Control del tiempo entre ataques
    private bool isAttacking = false;

    void Update()
    {
        // Calcular la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Verificar si el jugador est� dentro del rango de detecci�n
        if (distanceToPlayer <= detectionRange)
        {
            playerInRange = true;

            // Si no est� atacando, seguir al jugador
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

        // Si el jugador est� en rango de ataque y ha pasado el tiempo de cooldown, atacar
        if (playerInRange && distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown; // Reiniciar el cooldown
        }
    }

    void FollowPlayer()
    {
        // Obtener la direcci�n hacia el jugador en el eje X
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

            // Activar la animaci�n de caminar
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

        // Iniciar la animaci�n de ataque
        animator.SetTrigger("Punching");

        isAttacking = true;
        Invoke("ResumeMovement", stopDuration); // Reanudar el movimiento despu�s de `stopDuration`

        // Aplicar da�o al jugador (aqu� debes tener un script que controle la salud del jugador)
        if (player.GetComponent<PlayerHealth>() != null)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage); // Suponiendo que el jugador tiene un m�todo TakeDamage()
        }
    }

    void ResumeMovement()
    {
        // Reanudar el movimiento del enemigo
        isAttacking = false;
    }

    void StartWalking()
    {
        animator.SetBool("isWalking", true); // Activar la animaci�n de caminar
    }

    void StopWalking()
    {
        animator.SetBool("isWalking", false); // Desactivar la animaci�n de caminar
    }

    // Opcional: Mostrar el rango de detecci�n y ataque en la escena para visualizaci�n
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Rango de detecci�n
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Rango de ataque
    }
}