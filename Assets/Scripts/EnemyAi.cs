using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator animator; // Referencia al Animator del enemigo
    public Transform player; // Referencia al jugador
    public float detectionRange = 5f; // Rango de detección del jugador
    public float attackCooldown = 3f; // Tiempo de espera entre ataques
    public int attackDamage = 10; // Daño que inflige el enemigo

    private bool playerInRange = false;
    private float nextAttackTime = 0f; // Control del tiempo entre ataques

    void Update()
    {
        // Calcular la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Verificar si el jugador está dentro del rango de detección
        if (distanceToPlayer <= detectionRange)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        // Si el jugador está en rango y ha pasado el tiempo de cooldown, atacar
        if (playerInRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown; // Reiniciar el cooldown
        }
    }

    void Attack()
    {
        // Iniciar la animación de ataque
        animator.SetTrigger("Punching");

        // Aplicar daño al jugador (aquí debes tener un script que controle la salud del jugador)
        player.GetComponent<PlayerHealth>().TakeDamage(attackDamage); // Suponiendo que el jugador tiene un método TakeDamage()
    }

    // Opcional: Mostrar el rango de detección en la escena para visualización
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}