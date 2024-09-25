using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator; // Referencia al Animator
    public Transform attackPoint; // Punto desde donde se realizar� el ataque
    public float attackRange = 0.5f; // Rango del ataque
    public int attackDamage = 10; // Da�o del ataque
    public LayerMask enemyLayers; // Capa de los enemigos

    void Update()
    {
        // Detectar si se ha presionado la tecla de ataque (puedes cambiar la tecla "Fire1" por otra)
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Activar la animaci�n de golpe
        animator.SetTrigger("Punching");

        // Detectar enemigos en el rango de ataque
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // Aplicar da�o a los enemigos
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy")) // Aseg�rate de que el enemigo tenga la etiqueta "Enemy"
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null) // Verificar que el enemigo tenga el componente EnemyHealth
                {
                    enemyHealth.TakeDamage(attackDamage);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibujar el rango de ataque en la escena para visualizar el �rea donde se har� da�o
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
