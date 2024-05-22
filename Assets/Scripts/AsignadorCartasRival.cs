using UnityEngine;
using UnityEngine.UI;

public class AsignadorCartasRival : MonoBehaviour
{
    public GameObject[] cartas; // Arreglo de GameObjects que representan las cartas
    public GameObject[] cartas2; // Arreglo de GameObjects que representan las cartas
    public Sprite[] imagenes; // Arreglo de imágenes de las cartas
    public Sprite[] imagenes2; // Arreglo de imágenes de las cartas

    void Start()
    {
        for(int i = 0; i < cartas.Length; i++)
        {
            cartas[i].SetActive(true);
            cartas2[i].SetActive(true);
        }
        AsignarCartas();
    }

    public void AsignarCartas()
    {
        if (cartas.Length != 9 || imagenes.Length != 27)
        {
            Debug.LogError("El número de cartas o imágenes no es correcto. Asegúrate de que haya 9 cartas y 27 imágenes.");
            return;
        }

        // Establecer una semilla única para la generación aleatoria
        Random.InitState(System.DateTime.Now.Millisecond);

        int rangoInicial = 0;
        int rangoFinal = 2;

        for (int i = 0; i < cartas.Length; i++)
        {
            // Generar un índice aleatorio dentro del rango correspondiente
            int indiceAleatorio = Random.Range(rangoInicial, rangoFinal + 1);

            // Obtener el componente Carta del GameObject actual
            Carta cartaComponent = cartas[i].GetComponent<Carta>();
            cartaComponent.imageComponent.sprite = imagenes2[indiceAleatorio];
            cartaComponent.valor = indiceAleatorio + 1;

            Carta cartaComponent2 = cartas2[i].GetComponent<Carta>();
            cartaComponent2.imageComponent.sprite = imagenes[indiceAleatorio];
            cartaComponent2.valor = indiceAleatorio + 1;

            // Actualizar los rangos para la próxima iteración
            rangoInicial += 3;
            rangoFinal += 3;
        }
    }
}
