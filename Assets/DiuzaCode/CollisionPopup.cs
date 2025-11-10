using UnityEngine;
using System.Collections;

public class CollisionPopup : MonoBehaviour
{
    [Header("Ventana a mostrar (UI Panel)")]
    public GameObject ventana;

    [Header("Tiempo visible (segundos)")]
    public float duracion = 3f;

    private bool yaActivado = false;

    private void Start()
    {
        // Asegura que la ventana esté oculta al inicio
        if (ventana != null)
            ventana.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Evita repetir el evento si ya fue activado una vez
        if (yaActivado) return;

        // Verifica que el que entra sea el Player (puedes ajustar el tag según tu objeto)
        if (other.CompareTag("Player"))
        {
            yaActivado = true;

            // Enciende la ventana temporalmente
            if (ventana != null)
                StartCoroutine(MostrarVentanaTemporal());

            // Desactiva este objeto después de 1 frame (para que no se cancele la corrutina)
            StartCoroutine(DesactivarDespuesDeUnFrame());
        }
    }

    private IEnumerator MostrarVentanaTemporal()
    {
        ventana.SetActive(true);
        yield return new WaitForSeconds(duracion);
        ventana.SetActive(false);
    }

    private IEnumerator DesactivarDespuesDeUnFrame()
    {
        yield return null; // espera un frame
        gameObject.SetActive(false);
    }
}
