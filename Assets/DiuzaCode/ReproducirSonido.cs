using UnityEngine;
using UnityEngine.UI;

public class ReproducirSonido : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoClick; // Asigna el clip desde el inspector
    private AudioSource audioSource;

    void Start()
    {
        // Agrega o encuentra un AudioSource en el mismo objeto
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Configura el botón para reproducir sonido al hacer clic
        Button btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.AddListener(ReproducirClick);
    }

    void ReproducirClick()
    {
        if (sonidoClick != null)
            audioSource.PlayOneShot(sonidoClick);
        else
            Debug.LogWarning("No se asignó ningún sonido al botón: " + gameObject.name);
    }
}
