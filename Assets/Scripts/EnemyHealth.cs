using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50; // Salud m�xima del enemigo
    private int currentHealth; // Salud actual del enemigo

    void Start()
    {
        // Iniciar con la salud m�xima
        currentHealth = maxHealth;
    }

    // M�todo para recibir da�o
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reducir la salud
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegurarse de que la salud no sea menor a 0

        Debug.Log("Enemigo recibi� " + damage + " de da�o. Salud actual: " + currentHealth);

        // Verificar si el enemigo ha muerto
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // M�todo que se llama cuando el enemigo muere
    void Die()
    {
        // Aqu� puedes a�adir efectos de muerte, animaciones, etc.
        Debug.Log("El enemigo ha muerto.");

        // Desactivar el enemigo
        gameObject.SetActive(false);

        // Opcional: Destruir el objeto enemigo despu�s de un tiempo
        // Destroy(gameObject, 2f); // Puedes destruir el enemigo si lo prefieres
    }
}
