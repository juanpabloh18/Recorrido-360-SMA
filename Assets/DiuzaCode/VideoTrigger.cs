using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoTrigger : MonoBehaviour
{
    [Header("Referencias UI")]
    public Button botonAbrir;
    public GameObject panelVideo;
    public Button botonCerrar;
    public Button botonPlayPause;
    public VideoPlayer videoPlayer;

    private bool dentro = false;
    private bool isPlaying = false;

    void Start()
    {
        botonAbrir.gameObject.SetActive(false);
        panelVideo.SetActive(false);

        botonAbrir.onClick.AddListener(AbrirPanel);
        botonCerrar.onClick.AddListener(CerrarPanel);
        botonPlayPause.onClick.AddListener(TogglePlayPause);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que tu player tenga el tag "Player"
        {
            dentro = true;
            botonAbrir.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dentro = false;
            botonAbrir.gameObject.SetActive(false);
            CerrarPanel();
        }
    }

    void AbrirPanel()
    {
        if (!dentro) return;

        panelVideo.SetActive(true);
        videoPlayer.Play();
        isPlaying = true;
    }

    void CerrarPanel()
    {
        videoPlayer.Stop();
        panelVideo.SetActive(false);
        isPlaying = false;
    }

    void TogglePlayPause()
    {
        if (!isPlaying)
        {
            videoPlayer.Play();
            isPlaying = true;
        }
        else
        {
            videoPlayer.Pause();
            isPlaying = false;
        }
    }
}
