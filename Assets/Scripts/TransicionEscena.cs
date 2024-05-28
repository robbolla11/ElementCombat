using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicionEscena : MonoBehaviour
{

    private Animator animator;
    [SerializeField] private AnimationClip animationClip;
    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Menu()
    {
        StartCoroutine(CambiarEscena0());
    }

    public void EscenaRonda1()
    {
        StartCoroutine(CambiarEscena1());
    }

    public void EscenaRonda2()
    {
        StartCoroutine(CambiarEscena2());
    }

    public void EscenaRonda3()
    {
        StartCoroutine(CambiarEscena3());
    }

    public void Victoria()
    {
        StartCoroutine(CambiarEscena4());
    }

    public void Derrota()
    {
        StartCoroutine(CambiarEscena5());
    }

    public void Empate()
    {
        StartCoroutine(CambiarEscena6());
    }

    IEnumerator CambiarEscena0()
    {
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animationClip.length);
        SceneManager.LoadScene(0);
    }

    IEnumerator CambiarEscena1()
    {
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animationClip.length);
        SceneManager.LoadScene(1);
    }

    IEnumerator CambiarEscena2()
    {
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animationClip.length);
        SceneManager.LoadScene(2);
    }

    IEnumerator CambiarEscena3()
    {
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animationClip.length);
        SceneManager.LoadScene(3);
    }

    IEnumerator CambiarEscena4()
    {
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animationClip.length);
        SceneManager.LoadScene(4);
    }

    IEnumerator CambiarEscena5()
    {
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animationClip.length);
        SceneManager.LoadScene(5);
    }

    IEnumerator CambiarEscena6()
    {
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animationClip.length);
        SceneManager.LoadScene(6);
    }
}
