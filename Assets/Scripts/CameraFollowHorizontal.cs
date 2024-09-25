using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowHorizontal : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float smoothSpeed = 0.125f; // Velocidad de suavizado del movimiento
    public Vector3 offset; // Ajuste de posici�n manual en el Inspector

    void LateUpdate()
    {
        // Mantener la posici�n Y y Z de la c�mara fija, y solo seguir en X
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);

        // Suavizar el movimiento
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Aplicar la nueva posici�n suavizada
        transform.position = smoothedPosition;
    }
}