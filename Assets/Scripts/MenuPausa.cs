
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Asegúrate de tener esto para gestionar escenas

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject menuOpciones;

    [SerializeField] AudioSource backgroundSound;
    private bool juegoPausado = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Continuar();
            }
            else
            {
                Pausa();
            }
        }
    }

    public void Pausa()
    {
        AudioManager.Instance.StopSonido();
        juegoPausado = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        menuOpciones.SetActive(false);
        backgroundSound.Pause();
    }

    public void Continuar()
    {
        AudioManager.Instance.ResumeSonido();
        juegoPausado = false;
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        menuOpciones.SetActive(false);
        backgroundSound.Play();
    }

    public void Opciones()
    {
        juegoPausado = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(false);
        menuOpciones.SetActive(true);
    }

    public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }


}