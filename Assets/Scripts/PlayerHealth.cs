using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Salud m�xima del jugador
    public int currentHealth; // Salud actual del jugador

    // Evento de salud para efectos visuales o interfaces
    public delegate void OnHealthChanged(int health);
    public event OnHealthChanged onHealthChanged;

    void Start()
    {
        // Iniciar con la salud m�xima
        currentHealth = maxHealth;
        // Notificar el cambio inicial de salud
        onHealthChanged?.Invoke(currentHealth);
    }

    // M�todo para recibir da�o
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reducir la salud
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Evitar que la salud sea menor a 0 o mayor a la salud m�xima

        // Notificar el cambio de salud a quien est� suscrito
        onHealthChanged?.Invoke(currentHealth);

        // Si la salud llega a 0, el jugador muere
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // M�todo para que el jugador muera
    void Die()
    {
        // Aqu� puedes poner lo que sucede cuando el jugador muere (animaci�n de muerte, reinicio del nivel, etc.)
        Debug.Log("El jugador ha muerto.");
        // Por ejemplo, podr�as desactivar al jugador
        gameObject.SetActive(false);
    }

    // Opcional: M�todo para curar al jugador
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Evitar que la salud sobrepase el m�ximo
        onHealthChanged?.Invoke(currentHealth); // Notificar el cambio de salud
    }
}