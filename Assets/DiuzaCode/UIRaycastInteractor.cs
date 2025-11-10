using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class UIRaycastInteractor : MonoBehaviour
{
    [Header("Configuración")]
    public KeyCode interactKey = KeyCode.Mouse0;         // tecla para "click" (puedes usar KeyCode.E)
    public Image reticle;                                // retículo opcional (Image en Canvas)
    public float reticleHoverScale = 1.2f;               // efecto cuando apuntamos UI
    public float maxPhysicsDistance = 10f;               // distancia para raycast físico (3D buttons)

    GraphicRaycaster[] graphicRaycasters;
    PointerEventData pointerData;
    List<RaycastResult> uiResults = new List<RaycastResult>();
    GameObject lastHitUI;

    void Awake()
    {
        if (EventSystem.current == null)
        {
            Debug.LogError("[UIRaycastInteractor] No hay EventSystem en la escena. Crea uno: GameObject → UI → Event System");
        }

        // Coge todos los GraphicRaycasters (uno por Canvas)
        var allCanvases = FindObjectsOfType<Canvas>();
        var list = new List<GraphicRaycaster>();
        foreach (var c in allCanvases)
        {
            var gr = c.GetComponent<GraphicRaycaster>();
            if (gr != null) list.Add(gr);
        }
        graphicRaycasters = list.ToArray();

        pointerData = new PointerEventData(EventSystem.current);
    }

    void Update()
    {
        Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        pointerData.position = center;

        // 1) Primer intento: raycast UI (GraphicRaycaster / EventSystem)
        bool hitUI = false;
        uiResults.Clear();

        // Si hay GraphicRaycasters (Canvas con GraphicRaycaster)
        if (graphicRaycasters != null && graphicRaycasters.Length > 0)
        {
            // Recolecta resultados de todos los GraphicRaycasters
            foreach (var gr in graphicRaycasters)
            {
                uiResults.Clear();
                gr.Raycast(pointerData, uiResults);
                if (uiResults.Count > 0)
                {
                    // Tomamos el primer resultado (el más frontal)
                    var rr = uiResults[0];
                    HandleUIHit(rr.gameObject);
                    hitUI = true;
                    break;
                }
            }
        }
        else
        {
            // Fallback: usar EventSystem.RaycastAll (útil si sólo tienes Screen Space)
            var tmp = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, tmp);
            if (tmp.Count > 0)
            {
                HandleUIHit(tmp[0].gameObject);
                hitUI = true;
            }
        }

        // 2) Si no golpeó UI, intentamos Physics.Raycast para botones 3D con colliders
        if (!hitUI)
        {
            if (reticle != null) ResetReticle();
            lastHitUI = null;

            Ray ray = GetComponent<Camera>().ScreenPointToRay(center);
            if (Physics.Raycast(ray, out RaycastHit phHit, maxPhysicsDistance))
            {
                // Si el objeto tiene un Button en sí o en algún padre
                var btn = phHit.collider.GetComponentInParent<Button>();
                if (btn != null)
                {
                    // Podemos invocar el onClick si se presiona la tecla
                    if (Input.GetKeyDown(interactKey))
                    {
                        Debug.Log("[UIRaycastInteractor] Physics button invoked: " + btn.name);
                        btn.onClick.Invoke();
                    }

                    // visual
                    if (reticle != null) SetReticleHover();
                }
            }
        }
        else
        {
            // Si está mirando una UI y presionó la tecla, dispara el onClick del Button encontrado
            if (Input.GetKeyDown(interactKey) && lastHitUI != null)
            {
                var button = lastHitUI.GetComponentInParent<Button>();
                if (button != null)
                {
                    Debug.Log("[UIRaycastInteractor] UI button invoked: " + button.name);
                    button.onClick.Invoke();
                }
            }
        }
    }

    void HandleUIHit(GameObject hit)
    {
        lastHitUI = hit;

        // Asegurarse de obtener el botón (puede ser hijo de la imagen)
        var button = hit.GetComponentInParent<Button>();

        if (reticle != null)
            SetReticleHover();

        if (button != null)
        {
            // (opcional) highlight visual - podrías cambiar color o animar aquí
            // Debug
            // Debug.Log("[UIRaycastInteractor] UI hit on button: " + button.name);
        }
    }

    void SetReticleHover()
    {
        if (reticle == null) return;
        reticle.transform.localScale = Vector3.one * reticleHoverScale;
        // color ejemplo
        reticle.color = Color.cyan;
    }

    void ResetReticle()
    {
        if (reticle == null) return;
        reticle.transform.localScale = Vector3.one;
        reticle.color = Color.white;
    }
}
