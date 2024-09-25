using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float followDistance = 5f; // Distancia a la que el boss empezar� a seguir al jugador
    public float attackRange = 1f; // Rango de ataque
    public float attackCooldown = 5f; // Tiempo entre ataques
    public float speed = 2f; // Velocidad del boss
    public float hurricaneKickSpeed = 4f; // Velocidad durante el Hurricane Kick
    public Animator animator; // Referencia al Animator

    public float hurricaneKickDuration = 4f; // Duraci�n del Hurricane Kick, configurable desde el inspector
    public float idleDuration = 3f; // Duraci�n de la animaci�n de idle despu�s de 3 golpes

    private float nextAttackTime = 0f; // Temporizador para el siguiente ataque
    private int attackCount = 0; // Contador de ataques al jugador
    private bool isHurricaneKicking = false; // Para verificar si est� en el Hurricane Kick
    private float hurricaneKickTimer = 0f; // Temporizador para el Hurricane Kick
    private float idleTimer = 0f; // Temporizador para la animaci�n de idle

    void Update()
    {
        // Calcular la distancia entre el boss y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Seguir al jugador si est� dentro de la distancia de seguimiento
        if (distanceToPlayer < followDistance)
        {
            if (isHurricaneKicking)
            {
                ExecuteHurricaneKick();
            }
            else
            {
                // Solo seguir al jugador si no est� en idle
                if (attackCount < 3)
                {
                    FollowPlayer();

                    // Atacar si est� en rango y el tiempo de ataque ha pasado
                    if (distanceToPlayer < attackRange && Time.time >= nextAttackTime)
                    {
                        Attack();
                    }
                }
            }
        }

        // Mantener la rotaci�n del boss solo en el eje Y
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Ignorar la rotaci�n en Y
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        // Manejar el temporizador del Hurricane Kick
        if (isHurricaneKicking)
        {
            hurricaneKickTimer += Time.deltaTime;
            if (hurricaneKickTimer >= hurricaneKickDuration)
            {
                isHurricaneKicking = false; // Reiniciar estado al finalizar el ataque
                hurricaneKickTimer = 0f; // Reiniciar el temporizador
                attackCount = 0; // Reiniciar el contador de ataques
                animator.SetBool("IsWalking", true); // Volver a caminar despu�s del ataque
            }
        }

        // Manejar el temporizador del idle despu�s de 3 ataques
        if (attackCount >= 3 && !isHurricaneKicking)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleDuration)
            {
                isHurricaneKicking = true; // Activar el ataque Hurricane Kick
                idleTimer = 0f; // Reiniciar el temporizador
                speed = hurricaneKickSpeed; // Cambiar la velocidad para el Hurricane Kick
                animator.SetBool("IsWalking", false); // Detener la animaci�n de caminar
            }
            else
            {
                // No hacer nada mientras est� en idle
                speed = 0; // Detener al boss
                animator.SetBool("IsIdle", true); // Hacer idle
            }
        }
        else
        {
            animator.SetBool("IsIdle", false); // No estar en idle si hay ataques disponibles
        }
    }

    private void FollowPlayer()
    {
        // Moverse hacia el jugador solo si no est� en el Hurricane Kick
        if (!isHurricaneKicking)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Reproducir animaci�n de caminar
            animator.SetBool("IsWalking", true);
        }
    }

    private void Attack()
    {
        // Ejecutar ataque normal
        animator.SetTrigger("MutantPunch");
        attackCount++; // Incrementar el contador de ataques

        // Configurar el tiempo para el pr�ximo ataque
        nextAttackTime = Time.time + attackCooldown;

        // Si el contador de ataques llega a 3, detenerse antes del Hurricane Kick
        if (attackCount >= 3)
        {
            speed = 0; // Detener al boss
            animator.SetBool("IsWalking", false); // Detener la animaci�n de caminar
            idleTimer = 0f; // Reiniciar el temporizador de idle
        }

        // Aqu� puedes agregar l�gica para aplicar da�o al jugador
        // Ejemplo: player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
    }

    private void ExecuteHurricaneKick()
    {
        // Moverse hacia el jugador m�s r�pido durante el Hurricane Kick
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * hurricaneKickSpeed * Time.deltaTime;

        // Reproducir animaci�n de Hurricane Kick
        animator.SetTrigger("HurricaneKick"); // Aseg�rate de tener esta animaci�n configurada

        // Aqu� puedes agregar l�gica para aplicar da�o al jugador con el Hurricane Kick
        // Ejemplo: player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujar los c�rculos de distancia en la vista de escena
        Gizmos.color = Color.blue; // Color para la distancia de seguimiento
        Gizmos.DrawWireSphere(transform.position, followDistance);

        Gizmos.color = Color.red; // Color para el rango de ataque
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}