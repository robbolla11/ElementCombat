using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa; // Cambiado de private a public para poder asignarlo directamente desde el Inspector
    public GameObject desactivar;

    public SpriteRenderer spriteRenderer;

    public TransicionEscena transicionEscena;

    AudioManager audioManager;

    private bool puedePausar = false;

    private void Start()
    {
        if (menuPausa == null)
        {
            Debug.LogError("El objeto de menú de pausa no está asignado en el Inspector.");
        }
        menuPausa.SetActive(false);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        // Inicia la coroutine para habilitar la pausa después de 5 segundos
        StartCoroutine(HabilitarPausa());
    }

    private void Update()
    {
        if (puedePausar && Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0f)
            {
                Reanudar();
            }
            else
            {
                Pausa();
            }
        }
    }

    private IEnumerator HabilitarPausa()
    {
        // Espera 5 segundos
        yield return new WaitForSeconds(2f);
        puedePausar = true;
    }

    public void Pausa()
    {
        audioManager.playSFX(audioManager.click2, 0.65f);
        Time.timeScale = 0f;
        menuPausa.SetActive(true);
        desactivar.SetActive(false);
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 0.68f; 
            spriteRenderer.color = color;
        }
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        audioManager.playSFX(audioManager.click2, 0.65f);
        menuPausa.SetActive(false);
        desactivar.SetActive(true);
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 1f; // 190/255 = 0.75f
            spriteRenderer.color = color;
        }
    }

    public void MenuInicio()
    {
        Time.timeScale = 1f;
        audioManager.playSFX(audioManager.click2, 0.65f);
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
