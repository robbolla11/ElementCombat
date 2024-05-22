using UnityEngine;
using UnityEngine.UI;

public class Carta : MonoBehaviour
{
    public int valor; // Valor de la carta
    public Image imageComponent; // Componente de imagen

    void Awake()
    {
        imageComponent = GetComponent<Image>();
    }
}
