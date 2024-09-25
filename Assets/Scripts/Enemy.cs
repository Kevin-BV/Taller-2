using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Aquí puedes poner la lógica para la muerte del enemigo
        Destroy(gameObject); // Esto destruye al enemigo
    }
}
