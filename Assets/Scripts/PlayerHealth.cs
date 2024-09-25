using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Salud máxima del jugador
    public int currentHealth; // Salud actual del jugador

    // Evento de salud para efectos visuales o interfaces
    public delegate void OnHealthChanged(int health);
    public event OnHealthChanged onHealthChanged;

    void Start()
    {
        // Iniciar con la salud máxima
        currentHealth = maxHealth;
        // Notificar el cambio inicial de salud
        onHealthChanged?.Invoke(currentHealth);
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reducir la salud
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Evitar que la salud sea menor a 0 o mayor a la salud máxima

        // Notificar el cambio de salud a quien esté suscrito
        onHealthChanged?.Invoke(currentHealth);

        // Si la salud llega a 0, el jugador muere
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método para que el jugador muera
    void Die()
    {
        // Aquí puedes poner lo que sucede cuando el jugador muere (animación de muerte, reinicio del nivel, etc.)
        Debug.Log("El jugador ha muerto.");
        // Por ejemplo, podrías desactivar al jugador
        gameObject.SetActive(false);
    }

    // Opcional: Método para curar al jugador
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Evitar que la salud sobrepase el máximo
        onHealthChanged?.Invoke(currentHealth); // Notificar el cambio de salud
    }
}