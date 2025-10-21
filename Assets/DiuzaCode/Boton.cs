using UnityEngine;
using UnityEngine.UI;

public class Boton : MonoBehaviour
{
    [SerializeField] private GameObject ventanaInfo; // Asigna el panel desde el Inspector

    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.AddListener(AccionBoton);
    }

    void AccionBoton()
    {
        // Verifica si el botón tiene el tag "Info"
        if (CompareTag("Info"))
        {
            if (ventanaInfo != null)
            {
                bool estado = !ventanaInfo.activeSelf;
                ventanaInfo.SetActive(estado);
                Debug.Log("Ventana de información " + (estado ? "abierta" : "cerrada"));
            }
            else
            {
                Debug.LogWarning("No se asignó una ventana de información al botón " + gameObject.name);
            }
        }
    }
}
