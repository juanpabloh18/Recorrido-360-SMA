using UnityEngine;
using UnityEngine.UI;

public class InventarioUI : MonoBehaviour
{
    public Text textoContador;
    public Image CoBoule1;
    public Image CoBoule2;
    public Image CoSanAn;
    public Image CoSanAn2;

    private void Start()
    {
        var inv = InventarioGlobal.instancia;

        // Actualiza el contador global
        textoContador.text = "" + inv.objetosRecolectados;

        // Activa las imágenes según los objetos encontrados
        CoBoule1.enabled = inv.CoBoule1Encontrado;
        CoBoule2.enabled = inv.CoBoule2Encontrado;
        CoSanAn.enabled = inv.CoSanAnEncontrado;
        CoSanAn2.enabled = inv.CoSanAn2Encontrado;
    }
}
