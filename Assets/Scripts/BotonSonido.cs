using UnityEngine;
using System.Collections;

public class BotonSonido : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip primerSonido;
    public AudioClip segundoSonido;

    private Coroutine reproduccionActual;

    // 👉 Llamar a esta función desde el botón principal
    public void ReproducirSonidos()
    {
        // Si ya está reproduciendo algo, lo reiniciamos
        if (reproduccionActual != null)
            StopCoroutine(reproduccionActual);

        reproduccionActual = StartCoroutine(ReproducirEnSecuencia());
    }

    // 👉 Corrutina para reproducir los dos sonidos uno tras otro
    private IEnumerator ReproducirEnSecuencia()
    {
        if (primerSonido != null)
        {
            audioSource.clip = primerSonido;
            audioSource.Play();
            yield return new WaitForSeconds(primerSonido.length);
        }

        if (segundoSonido != null)
        {
            audioSource.clip = segundoSonido;
            audioSource.Play();
            yield return new WaitForSeconds(segundoSonido.length);
        }

        reproduccionActual = null;
    }

    // 👉 Llamar a esta función desde el botón de “Cerrar” o “Detener”
    public void DetenerSonido()
    {
        if (reproduccionActual != null)
        {
            StopCoroutine(reproduccionActual);
            reproduccionActual = null;
        }

        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
