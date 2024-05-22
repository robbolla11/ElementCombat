using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AsignadorCartas : MonoBehaviour
{
    public GameObject[] cartas; // Arreglo de GameObjects que representan las cartas
    public Sprite[] imagenes; // Arreglo de imágenes de las cartas

    private List<int> indicesAleatorios = new List<int>(); // Lista para almacenar los índices aleatorios

    void Start()
    {
        for(int i = 0; i < cartas.Length; i++)
        {
            cartas[i].SetActive(true);
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

        // Limpiar la lista de índices aleatorios para evitar duplicados si se llama más de una vez
        indicesAleatorios.Clear();

        // Establecer una semilla única para la generación aleatoria
        Random.InitState(System.DateTime.Now.Millisecond);

        int rangoInicial = 0;
        int rangoFinal = 2;

        for (int i = 0; i < cartas.Length; i++)
        {
            // Generar un índice aleatorio dentro del rango correspondiente
            int indiceAleatorio = Random.Range(rangoInicial, rangoFinal + 1);

            // Asegurarse de que no se repitan las cartas del mismo rango
            while (indicesAleatorios.Contains(indiceAleatorio))
            {
                indiceAleatorio = Random.Range(rangoInicial, rangoFinal + 1);
            }
            indicesAleatorios.Add(indiceAleatorio); // Agregar el índice aleatorio a la lista

            // Obtener el componente Carta del GameObject actual
            Carta cartaComponent = cartas[i].GetComponent<Carta>();

            // Asignar la imagen y el valor al componente Carta
            cartaComponent.imageComponent.sprite = imagenes[indiceAleatorio];
            cartaComponent.valor = indiceAleatorio + 1;

            // Actualizar los rangos para la próxima iteración
            rangoInicial += 3;
            rangoFinal += 3;
        }
    }
}
