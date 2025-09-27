using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordToggle : MonoBehaviour
{
    [Header("Referencias")]
    public TMP_InputField inputPassword;
    public Button botonToggle;
    public Image iconoToggle;

    [Header("Iconos")]
    public Sprite ojoAbierto;
    public Sprite ojoCerrado;

    private bool mostrando = false;

    void Start()
    {
        // Al inicio debe estar oculto
        SetEstado(false);

        // Asignamos el evento del botón
        botonToggle.onClick.AddListener(TogglePassword);
    }

    void TogglePassword()
    {
        mostrando = !mostrando;
        SetEstado(mostrando);
    }

    void SetEstado(bool mostrar)
    {
        if (mostrar)
        {
            inputPassword.contentType = TMP_InputField.ContentType.Standard; // texto normal
            iconoToggle.sprite = ojoAbierto;
        }
        else
        {
            inputPassword.contentType = TMP_InputField.ContentType.Password; // oculto con ****
            iconoToggle.sprite = ojoCerrado;
        }

        // Refresca el campo para aplicar el cambio
        inputPassword.ForceLabelUpdate();
    }
}
