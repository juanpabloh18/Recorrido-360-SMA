using UnityEngine;
using UnityEngine.UI;

public class ModosVisualizacion : MonoBehaviour
{
    [Header("Referencias de UI")]
    [SerializeField] private Toggle toggleSonidosCulturales;
    [SerializeField] private Toggle toggleSubtitulos;

    [Header("Elementos del recorrido")]
    [SerializeField] private AudioSource audioCultural; // Sonido ambiente cultural
    [SerializeField] private GameObject panelSubtitulos; // Panel o texto de subtítulos

    private bool modoSonidosActivo = false;
    private bool modoSubtitulosActivo = false;

    void Start()
    {
        // Verifica si hay toggles asignados y les añade listeners
        if (toggleSonidosCulturales != null)
            toggleSonidosCulturales.onValueChanged.AddListener(ActivarSonidosCulturales);

        if (toggleSubtitulos != null)
            toggleSubtitulos.onValueChanged.AddListener(ActivarSubtitulos);

        // Asegura que ambos modos empiecen desactivados
        if (audioCultural != null)
            audioCultural.Stop();

        if (panelSubtitulos != null)
            panelSubtitulos.SetActive(false);
    }

    // 🔊 Activa o desactiva el modo de sonidos culturales
    void ActivarSonidosCulturales(bool activar)
    {
        modoSonidosActivo = activar;

        if (audioCultural != null)
        {
            if (activar)
            {
                audioCultural.Play();
                Debug.Log("Modo de sonidos culturales activado.");
            }
            else
            {
                audioCultural.Stop();
                Debug.Log("Modo de sonidos culturales desactivado.");
            }
        }
    }

    // 💬 Activa o desactiva el modo de subtítulos
    void ActivarSubtitulos(bool activar)
    {
        modoSubtitulosActivo = activar;

        if (panelSubtitulos != null)
        {
            panelSubtitulos.SetActive(activar);
            Debug.Log("Modo de subtítulos " + (activar ? "activado" : "desactivado") + ".");
        }
    }

    // 👁️ Método adicional para desactivar todo (Modo Normal)
    public void DesactivarTodosLosModos()
    {
        if (toggleSonidosCulturales != null)
            toggleSonidosCulturales.isOn = false;

        if (toggleSubtitulos != null)
            toggleSubtitulos.isOn = false;

        Debug.Log("Todos los modos de visualización desactivados (modo normal).");
    }
}
