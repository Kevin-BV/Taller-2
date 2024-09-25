using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100; // Salud máxima del jefe
    private int currentHealth; // Salud actual del jefe
    public Animator animator; // Referencia al Animator para controlar animaciones

    void Start()
    {
        // Inicializar la salud actual
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Mostrar la salud actual en el Debug
        Debug.Log("Current Health: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        // Reducir la salud actual al recibir daño
        currentHealth -= damage;

        // Mostrar un mensaje de depuración al recibir daño
        Debug.Log("Boss took damage: " + damage + ". Current Health: " + currentHealth);

        // Verificar si la salud actual es menor o igual a cero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Aquí puedes manejar lo que ocurre cuando el jefe muere
        Debug.Log("Boss has died."); // Mensaje de depuración al morir
        animator.SetTrigger("Die"); // Activar animación de muerte
        // Desactivar el jefe o manejar la lógica de muerte
        gameObject.SetActive(false); // Desactivar el objeto del jefe
        // Puedes agregar lógica adicional, como recompensas o transiciones de escena
    }
}