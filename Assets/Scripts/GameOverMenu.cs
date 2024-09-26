using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject reiniciar;
    [SerializeField] private GameObject menuGameOver;
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject menuOpciones;
    public static GameOverMenu Instance { get; private set; }
    [SerializeField] private AudioSource backgroundSound;

    private bool juegoPausado = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (backgroundSound != null)
        {
            backgroundSound.Play();
        }
    }

    public void Perder()
    {
        botonPausa.SetActive(false);
        menuPausa.SetActive(false);
        menuOpciones.SetActive(false);
        juegoPausado = true;
        Time.timeScale = 0f;
        reiniciar.SetActive(true);
        menuGameOver.SetActive(true);
        if (backgroundSound != null)
        {
            backgroundSound.Pause();
        }
    }

    public void Reiniciar()
    {
        juegoPausado = false;
        botonPausa.SetActive(true);
        menuPausa.SetActive(true);
        menuOpciones.SetActive(true);
        //GameManager.Instance.ResetPuntajeEscenaActual();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        if (backgroundSound != null)
        {
            backgroundSound.Play();
        }
    }

    public void MenuInicial(string nombre)
    {
        juegoPausado = false;
        //GameManager.Instance.ResetPuntajeEscenaActual();
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombre);
    }

    public void Salir()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        Application.Quit();
    }
}
