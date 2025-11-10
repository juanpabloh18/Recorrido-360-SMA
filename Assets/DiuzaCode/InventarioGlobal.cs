using UnityEngine;

public class InventarioGlobal : MonoBehaviour
{
    public static InventarioGlobal instancia;

    public int objetosRecolectados = 0;

    // Estado de cada objeto
    public bool CoBoule1Encontrado = false;
    public bool CoBoule2Encontrado = false;
    public bool CoSanAnEncontrado = false;
    public bool CoSanAn2Encontrado = false;

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

    // Método general para sumar objetos
    public void AgregarObjeto(string nombreObjeto)
    {
        switch (nombreObjeto)
        {
            case "CoBoule1":
                if (!CoBoule1Encontrado)
                {
                    CoBoule1Encontrado = true;
                    objetosRecolectados++;
                }
                break;

            case "CoBoule2":
                if (!CoBoule2Encontrado)
                {
                    CoBoule2Encontrado = true;
                    objetosRecolectados++;
                }
                break;

            case "CoSanAn":
                if (!CoSanAnEncontrado)
                {
                    CoSanAnEncontrado = true;
                    objetosRecolectados++;
                }
                break;

            case "CoSanAn2":
                if (!CoSanAn2Encontrado)
                {
                    CoSanAn2Encontrado = true;
                    objetosRecolectados++;
                }
                break;
        }
    }
}
