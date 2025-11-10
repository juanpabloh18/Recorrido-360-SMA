using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class WorldSpaceButtonRaycaster : MonoBehaviour
{
    public Camera cam; // La cámara que dispara el raycast

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenPoint = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

            Canvas[] canvases = FindObjectsOfType<Canvas>();
            foreach (Canvas canvas in canvases)
            {
                if (canvas.renderMode != RenderMode.WorldSpace) continue;

                GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();
                if (raycaster == null) continue;

                EventSystem eventSystem = EventSystem.current;
                PointerEventData pointerData = new PointerEventData(eventSystem)
                {
                    position = screenPoint
                };

                List<RaycastResult> results = new List<RaycastResult>();
                raycaster.Raycast(pointerData, results);

                foreach (RaycastResult result in results)
                {
                    Button b = result.gameObject.GetComponent<Button>();
                    if (b != null)
                    {
                        b.onClick.Invoke();
                        Debug.Log("Botón presionado: " + b.gameObject.name);
                    }
                }
            }
        }
    }
}
