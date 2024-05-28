using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject desactivar;
    public GameObject activar;
    public CameraMover cameraMover; // Referencia al script de movimiento de la cámara

    public TransicionEscena transicionEscena;

    AudioManager audioManager;


    private void Start()
    {
        // Obtener referencia al script de movimiento de la cámara
        cameraMover = FindObjectOfType<CameraMover>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Juego()
    {
        audioManager.playSFX(audioManager.click2, 0.65f);
        transicionEscena.EscenaRonda1();
    }

    public void HowTo()
    {
        audioManager.playSFX(audioManager.click2, 0.65f);
        desactivar.SetActive(false);
        activar.SetActive(true);
    }

    public void Back()
    {
        audioManager.playSFX(audioManager.click2, 0.65f);
        desactivar.SetActive(true);
        activar.SetActive(false);
    }

    public void Salir()
    {
        audioManager.playSFX(audioManager.click2, 0.65f);
        Application.Quit();
    }

    public void MenuInicio()
    {
        // Reactivar el script de movimiento de la cámara cuando regresas al menú principal
        audioManager.playSFX(audioManager.click2, 0.65f);
        if (cameraMover != null)
        {
            cameraMover.enabled = true;
        }
        reiniciar();
        transicionEscena.Menu();
    }
    void reiniciar()
    {
        GameManager.currentRound = 0;

        GameManager.ganadasPlayer = 0;
        GameManager.ganadasEnemy = 0;

        GameManager.empatesRondas = 0;
        GameManager.estrella1Color = Color.white;
        GameManager.estrella2Color = Color.white;
        GameManager.estrella3Color = Color.white;
    }
}
