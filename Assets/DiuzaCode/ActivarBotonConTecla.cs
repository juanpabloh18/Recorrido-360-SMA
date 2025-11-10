using UnityEngine;
using UnityEngine.UI;

public class ActivarBotonConTecla : MonoBehaviour
{
    public Button botonUI; // El botón que se quiere activar
    public KeyCode tecla = KeyCode.K; // Puedes cambiarla desde el inspector

    void Update()
    {
        // Detecta si se presiona la tecla K
        if (Input.GetKeyDown(tecla))
        {
            // Ejecuta el evento del botón como si se hubiera presionado
            if (botonUI != null)
            {
                botonUI.onClick.Invoke();
                Debug.Log("Botón activado con la tecla " + tecla);


            }
            else
            {
                Debug.LogWarning("No hay botón asignado en el script.");
            }
        }
    }
}
