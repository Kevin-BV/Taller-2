using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixerMusica;
    [SerializeField] private AudioMixer audioMixerSFX;
    [SerializeField] private Slider sliderMusica;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Toggle togglePantallaCompleta;

    private void Start()
    {
        CargarPreferencias();
    }

    public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
        PlayerPrefs.SetInt("PantallaCompleta", pantallaCompleta ? 1 : 0);
    }

    public void CambiarVolumenMusica(float volumen)
    {
        audioMixerMusica.SetFloat("VolumenMusica", volumen);
        PlayerPrefs.SetFloat("VolumenMusica", volumen);
    }

    public void CambiarVolumenSFX(float volumen)
    {
        audioMixerSFX.SetFloat("VolumenSFX", volumen);
        PlayerPrefs.SetFloat("VolumenSFX", volumen);
    }

    private void CargarPreferencias()
    {
        // Cargar y aplicar la configuración de pantalla completa
        bool pantallaCompleta = PlayerPrefs.GetInt("PantallaCompleta", 1) == 1;
        Screen.fullScreen = pantallaCompleta;
        togglePantallaCompleta.isOn = pantallaCompleta;

        // Cargar y aplicar el volumen de la música
        if (PlayerPrefs.HasKey("VolumenMusica"))
        {
            float volumenMusica = PlayerPrefs.GetFloat("VolumenMusica");
            audioMixerMusica.SetFloat("VolumenMusica", volumenMusica);
            sliderMusica.value = volumenMusica;
        }

        // Cargar y aplicar el volumen de los efectos de sonido
        if (PlayerPrefs.HasKey("VolumenSFX"))
        {
            float volumenSFX = PlayerPrefs.GetFloat("VolumenSFX");
            audioMixerSFX.SetFloat("VolumenSFX", volumenSFX);
            sliderSFX.value = volumenSFX;
        }
    }
}