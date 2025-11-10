using UnityEngine;

public class UsuarioSesion : MonoBehaviour
{
    public static UsuarioSesion instancia;

    public string nombreCompleto;
    public string correoUsuario;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GuardarDatos(string nombre, string correo)
    {
        nombreCompleto = nombre;
        correoUsuario = correo;
    }
}
