using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Importar la librería para usar listas
using UnityEngine.UI;


public class CardSelection : MonoBehaviour
{
    private GameManager gameManager;

    private Vector3 originalScale;
    public float hoverScaleFactor = 1.2f; // Factor por el cual se agranda la carta al pasar el mouse
    public float animationDuration = 0.2f; // Duración de la animación

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        originalScale = transform.localScale; // Guardar la escala original de la carta
    }

    void OnMouseDown()
    {
        if (!gameManager.IsPlayerTurn)
            return;

        gameManager.SelectPlayerCard(gameObject);
    }

    void OnMouseEnter()
    {
        StopAllCoroutines(); // Detener cualquier otra animación que esté ocurriendo
        StartCoroutine(ScaleOverTime(transform, originalScale * hoverScaleFactor, animationDuration));
    }

    void OnMouseExit()
    {
        StopAllCoroutines(); // Detener cualquier otra animación que esté ocurriendo
        StartCoroutine(ScaleOverTime(transform, originalScale, animationDuration));
    }

    private IEnumerator ScaleOverTime(Transform target, Vector3 toScale, float duration)
    {
        Vector3 startScale = target.localScale;
        float time = 0f;
        
        while (time < duration)
        {
            target.localScale = Vector3.Lerp(startScale, toScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        
        target.localScale = toScale;
    }
}
