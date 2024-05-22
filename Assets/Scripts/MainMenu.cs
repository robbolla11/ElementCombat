using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject desactivar;
    public GameObject activar;
    public CameraMover cameraMover; // Referencia al script de movimiento de la cámara


    private void Start()
    {
        // Obtener referencia al script de movimiento de la cámara
        cameraMover = FindObjectOfType<CameraMover>();
    }

    public void Juego()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void HowTo()
    {
        desactivar.SetActive(false);
        activar.SetActive(true);
    }

    public void Back()
    {
        desactivar.SetActive(true);
        activar.SetActive(false);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void MenuInicio()
    {
        // Reactivar el script de movimiento de la cámara cuando regresas al menú principal
        if (cameraMover != null)
        {
            cameraMover.enabled = true;
        }
        reiniciar();
        SceneManager.LoadSceneAsync(0);
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
