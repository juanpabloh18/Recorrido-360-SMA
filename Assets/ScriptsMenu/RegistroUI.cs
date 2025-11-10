using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegistroUI : MonoBehaviour
{
    [Header("Referencias")]
    public UsuarioManager usuarioManager;

    [Header("Campos de entrada")]
    public TMP_InputField inputNombre;
    public TMP_InputField inputCorreo;
    public TMP_InputField inputPassword;
    public TMP_InputField inputConfirmar;

    [Header("Toggle")]
    public Toggle toggleAcepto;

    [Header("Botón")]
    public Button botonCrearCuenta;

    [Header("Mensajes")]
    public TextMeshProUGUI mensajeError;

    [Header("Paneles")]
    public GameObject panelCrearCuenta;
    public GameObject panelLogin;

    void Start()
    {
        botonCrearCuenta.interactable = false;
        SetButtonOpacity(0.6f);

        //El mensaje arranca oculto
        mensajeError.gameObject.SetActive(false);

        // Suscribimos validaciones
        inputNombre.onValueChanged.AddListener(delegate { ValidarFormulario(); });
        inputCorreo.onValueChanged.AddListener(delegate { ValidarFormulario(); });
        inputPassword.onValueChanged.AddListener(delegate { ValidarFormulario(); });
        inputConfirmar.onValueChanged.AddListener(delegate { ValidarFormulario(); });
        toggleAcepto.onValueChanged.AddListener(delegate { ValidarFormulario(); });
    }

    void ValidarFormulario()
    {
        
        mensajeError.gameObject.SetActive(false);

        
        if (string.IsNullOrWhiteSpace(inputNombre.text))
        {
            DesactivarBoton();
            return;
        }

        
        if (string.IsNullOrWhiteSpace(inputCorreo.text))
        {
            DesactivarBoton();
            return;
        }

       
        if (inputPassword.text.Length < 8)
        {
            DesactivarBoton();
            return;
        }

         
        if (inputPassword.text != inputConfirmar.text)
        {
            mensajeError.text = "Las contraseñas no coinciden.";
            mensajeError.gameObject.SetActive(true);
            DesactivarBoton();
            return;
        }

        
        if (!toggleAcepto.isOn)
        {
            DesactivarBoton();
            return;
        }

        
        ActivarBoton();
    }

    public void BotonCrearCuenta()
    {
        bool creado = usuarioManager.CrearUsuario(inputNombre.text, inputCorreo.text, inputPassword.text);

        if (creado)
        {
            Debug.Log("Usuario creado con éxito");

            mensajeError.gameObject.SetActive(false);

            //cerrar panel de creación y abrir panel inicial
            panelCrearCuenta.SetActive(false);
            panelLogin.SetActive(true);
        }
        else
        {
            Debug.Log("El correo ya está registrado");
            mensajeError.text = "El correo ya está registrado.";
            mensajeError.gameObject.SetActive(true);
        }
    }

    void ActivarBoton()
    {
        botonCrearCuenta.interactable = true;
        SetButtonOpacity(1f);
    }

    void DesactivarBoton()
    {
        botonCrearCuenta.interactable = false;
        SetButtonOpacity(0.6f);
    }

    void SetButtonOpacity(float alpha)
    {
        Color c = botonCrearCuenta.image.color;
        c.a = alpha;
        botonCrearCuenta.image.color = c;
    }
}
