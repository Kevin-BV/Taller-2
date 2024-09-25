using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPlay : MonoBehaviour
{
    // Este campo te permitirá editar el nombre de la escena desde el Inspector
    public string sceneName;

    // Este método se vincula con el botón "Jugar"
    public void LoadTutorialScene()
    {
        // Cargar la escena con el nombre asignado en el Inspector
        SceneManager.LoadScene(sceneName);
    }
}
