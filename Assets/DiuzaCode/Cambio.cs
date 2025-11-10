using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cambio : MonoBehaviour
{
    [SerializeField] private float tiempoDeEspera = 1f;
    private bool puedeCambiar = true;

    // Método público para cambiar de escena
    public void CambiarEscena(string nombreEscena)
    {
        if (!puedeCambiar)
        {
            Debug.Log("Cambio de escena en curso, espera un momento...");
            return;
        }

        StartCoroutine(CargarConRetardo(nombreEscena));
    }

    private IEnumerator CargarConRetardo(string nombreEscena)
    {
        puedeCambiar = false;

        Debug.Log("Cargando escena: " + nombreEscena);
        yield return new WaitForSeconds(tiempoDeEspera);

        if (Application.CanStreamedLevelBeLoaded(nombreEscena))
        {
            SceneManager.LoadScene(nombreEscena);
        }
        else
        {
            Debug.LogError("La escena '" + nombreEscena + "' no está en los Build Settings.");
        }

        puedeCambiar = true;
    }
}
