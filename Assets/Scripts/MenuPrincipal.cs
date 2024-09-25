using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public string escenaACargar;

    public void Jugar()
    {
        SceneManager.LoadScene(escenaACargar);
    }

    public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }

}
