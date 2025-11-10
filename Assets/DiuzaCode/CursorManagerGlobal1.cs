using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CursorManagerGlobal : MonoBehaviour
{
    public static CursorManagerGlobal instancia;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Forzamos el cursor varias veces (inmediato y al final del frame) por si otros scripts lo cambian
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // También forzamos al final del frame por seguridad
        StartCoroutine(ForceCursorEndOfFrame());
    }

    private IEnumerator ForceCursorEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
