using UnityEngine;
using TMPro;

public class MostrarDatosUsuario : MonoBehaviour
{
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoCorreo;

    void Start()
    {
        if (UsuarioSesion.instancia != null)
        {
            textoNombre.text = UsuarioSesion.instancia.nombreCompleto;
            textoCorreo.text = UsuarioSesion.instancia.correoUsuario;
        }
        else
        {
            textoNombre.text = "No hay usuario activo";
            textoCorreo.text = "";
        }
    }
}
