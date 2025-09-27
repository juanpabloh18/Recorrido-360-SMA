using UnityEngine;
using UnityEngine.SceneManagement;

public class InvitadoUI : MonoBehaviour
{
    // Este método lo asignas al botón "Entrar como Invitado"
    public void EntrarComoInvitado()
    {
        Debug.Log("Entrando como invitado");
        SceneManager.LoadScene("EscenaInicial");
    }
}
