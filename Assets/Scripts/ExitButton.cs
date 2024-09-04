using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // Este m�todo se llama cuando el jugador hace clic en el bot�n de salir
    public void ExitGame()
    {
        // Si estamos en el editor de Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si estamos en una build del juego
        Application.Quit();
#endif
    }
}