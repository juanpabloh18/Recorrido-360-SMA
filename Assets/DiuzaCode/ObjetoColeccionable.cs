using UnityEngine;
using UnityEngine.UI;

public class ObjetoColeccionable : MonoBehaviour
{
    public Text textoContador;
    public string nombreObjeto; // ← NUEVO: "CoBoule1", "CoBoule2", etc.

    public void AlHacerClick()
    {
        // Agrega el objeto al inventario global
        InventarioGlobal.instancia.AgregarObjeto(nombreObjeto);

        // Actualiza el texto del contador
        textoContador.text = "" + InventarioGlobal.instancia.objetosRecolectados;

        // Desactiva el botón (ya fue recogido)
        gameObject.SetActive(false);
    }
}
