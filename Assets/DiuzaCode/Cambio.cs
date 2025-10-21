using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cambio : MonoBehaviour
{
    // Variable para controlar si se puede cambiar de escena
    private bool puedeCambiar = true;

    // Tiempo de espera opcional antes de cambiar (por ejemplo, para mostrar una animación)
    [SerializeField] private float tiempoDeEspera = 1.5f;

    // Método público para cambiar de escena según el destino
    public void CambiarEscena(string destino)
    {
        if (!puedeCambiar)
        {
            Debug.Log("Cambio de escena en curso, espera un momento...");
            return;
        }

        destino = destino.ToLower().Trim(); // Estandariza el texto

        Debug.Log("Intentando ir a: " + destino);

        // Condicionales para cada punto del recorrido
        if (destino == "zoologico" || destino == "zoologico de cali")
        {
            Debug.Log("Cargando escena: ZoologicoDeCali");
            StartCoroutine(CargarConRetardo("ZoologicoDeCali"));
        }
        else if (destino == "san antonio")
        {
            Debug.Log("Cargando escena: SanAntonio");
            StartCoroutine(CargarConRetardo("SanAntonio"));
        }
        else if (destino == "boulevard" || destino == "bulevar")
        {
            Debug.Log("Cargando escena: Boulevard");
            StartCoroutine(CargarConRetardo("Boulevard"));
        }
        else if (destino == "cristo rey" || destino == "cristorey")
        {
            Debug.Log("Cargando escena: CristoRey");
            StartCoroutine(CargarConRetardo("CristoRey"));
        }
        else if (destino == "pance")
        {
            Debug.Log("Cargando escena: Pance");
            StartCoroutine(CargarConRetardo("Pance"));
        }
        else
        {
            Debug.LogWarning("El destino '" + destino + "' no coincide con ninguna escena registrada.");
        }
    }

    // Corrutina para simular una transición o animación antes del cambio
    private IEnumerator CargarConRetardo(string nombreEscena)
    {
        puedeCambiar = false;
        Debug.Log("Iniciando transición a la escena: " + nombreEscena);

        // Aquí podrías reproducir una animación, sonido o pantalla de carga
        yield return new WaitForSeconds(tiempoDeEspera);

        // Verifica si la escena existe antes de cargar
        if (Application.CanStreamedLevelBeLoaded(nombreEscena))
        {
            SceneManager.LoadScene(nombreEscena);
            Debug.Log("Escena cargada correctamente: " + nombreEscena);
        }
        else
        {
            Debug.LogError("La escena '" + nombreEscena + "' no está en los Build Settings.");
        }

        puedeCambiar = true;
    }
}
