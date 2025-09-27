using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginUI : MonoBehaviour
{
    [Header("Referencias")]
    public UsuarioManager usuarioManager;

    [Header("Campos de entrada")]
    public TMP_InputField inputCorreo;
    public TMP_InputField inputPassword;

    [Header("Botón")]
    public Button botonLogin;

    [Header("Mensajes")]
    public TextMeshProUGUI mensajeError;

    void Start()
    {
        botonLogin.onClick.AddListener(BotonLogin);

        // Mensaje inicia oculto
        if (mensajeError != null)
            mensajeError.gameObject.SetActive(false);
    }

    public void BotonLogin()
    {
        string correo = inputCorreo.text;
        string password = inputPassword.text;

        if (usuarioManager.IniciarSesion(correo, password))
        {
            Debug.Log("Inicio de sesión exitoso");

            if (mensajeError != null)
                mensajeError.gameObject.SetActive(false);

            // Cambiar a la escena inicial
            SceneManager.LoadScene("EscenaInicial");
        }
        else
        {
            Debug.Log("Correo o contraseña incorrectos");

            if (mensajeError != null)
            {
                mensajeError.text = "Correo o contraseña incorrectos.";
                mensajeError.gameObject.SetActive(true);
            }
        }
    }
}
