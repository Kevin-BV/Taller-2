using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50; // Salud máxima del enemigo
    private int currentHealth; // Salud actual del enemigo

    void Start()
    {
        // Iniciar con la salud máxima
        currentHealth = maxHealth;
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reducir la salud
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegurarse de que la salud no sea menor a 0

        Debug.Log("Enemigo recibió " + damage + " de daño. Salud actual: " + currentHealth);

        // Verificar si el enemigo ha muerto
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método que se llama cuando el enemigo muere
    void Die()
    {
        // Aquí puedes añadir efectos de muerte, animaciones, etc.
        Debug.Log("El enemigo ha muerto.");

        // Desactivar el enemigo
        gameObject.SetActive(false);

        // Opcional: Destruir el objeto enemigo después de un tiempo
        // Destroy(gameObject, 2f); // Puedes destruir el enemigo si lo prefieres
    }
}
