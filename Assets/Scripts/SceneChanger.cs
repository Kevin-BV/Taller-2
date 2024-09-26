using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement; // Necesario para cambiar escenas

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad; // Nombre de la escena que quieres cargar
    public float checkInterval = 1f; // Intervalo para verificar la existencia de enemigos

    private void Start()
    {
        // Iniciar la verificación en un coroutine
        StartCoroutine(CheckForEnemies());
    }

    private System.Collections.IEnumerator CheckForEnemies()
    {
        while (true)
        {
            // Buscar todos los objetos con el tag "Enemy"
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // Si no hay enemigos en la escena
            if (enemies.Length == 0)
            {
                // Cambiar a la escena especificada
                SceneManager.LoadScene(sceneToLoad);
                yield break; // Salir del coroutine
            }

            // Esperar el intervalo especificado antes de volver a comprobar
            yield return new WaitForSeconds(checkInterval);
        }
    }
}