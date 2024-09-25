using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingFollower : MonoBehaviour
{
    public Transform player; // Referencia al jugador que el objeto volador seguirá
    public float followDistance = 2f; // Distancia mínima a la que debe mantenerse
    public float followSpeed = 5f; // Velocidad a la que se moverá el objeto
    public Vector3 offset; // Desplazamiento adicional para ajustar la posición del objeto volador

    void Update()
    {
        // Calcular la dirección hacia el jugador
        Vector3 targetPosition = player.position + offset;

        // Calcular la distancia entre el objeto volador y el jugador
        float distance = Vector3.Distance(transform.position, player.position);

        // Si la distancia es mayor que la distancia mínima, mover el objeto volador hacia el jugador
        if (distance > followDistance)
        {
            // Moverse hacia el jugador con suavidad
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }

        // Opcional: Hacer que el objeto mire al jugador mientras lo sigue
        transform.LookAt(player);
    }
}