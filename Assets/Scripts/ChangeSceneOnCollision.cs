using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cargar escenas

public class ChangeSceneOnCollision : MonoBehaviour
{
    [Header("Escena a cargar al colisionar")]
    public string sceneToLoad; // Nombre de la escena que deseas cargar

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colisiona tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Cambiar a la escena especificada
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}