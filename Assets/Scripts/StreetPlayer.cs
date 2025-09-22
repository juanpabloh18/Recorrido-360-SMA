using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class StreetPlayer : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Pivot que rota en X (arriba/abajo). Suele ser el padre de la Main Camera.")]
    public Transform cameraPivot;

    [Tooltip("LayerMask del layer 'Waypoint'")]
    public LayerMask waypointLayer;

    [Header("Movement")]
    [Tooltip("Velocidad de desplazamiento hacia el waypoint")]
    public float moveSpeed = 4f;

    [Tooltip("Velocidad de giro hacia la orientación del waypoint")]
    public float rotateSpeed = 6f;

    [Tooltip("Distancia de parada al llegar al destino")]
    public float stopDistance = 0.03f;

    private CharacterController cc;
    private Coroutine movingRoutine;
    private Waypoint current;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        if (Camera.main == null)
            Debug.LogWarning("No hay cámara con tag 'MainCamera' en la escena. Camera.main será null.");
    }

    void Update()
    {
        //Disparador de movimiento
        if (!IsPointerOverUI())
        {
            if (Input.GetMouseButtonDown(0))
                TrySetDestinationFromCenter();

            if (Input.GetKeyDown(KeyCode.E)) 
                TrySetDestinationFromCenter();
        }

        if (Input.GetKeyDown(KeyCode.F1))
            DebugRayFromCenter();
    }


    void TrySetDestinationFromCenter() //Rayo para el destino 
    {
        if (Camera.main == null) return;

        Vector3 center = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(center);

        //Colliders con IsTrigger
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, waypointLayer, QueryTriggerInteraction.Collide))
        {
            Waypoint wp = hit.collider.GetComponentInParent<Waypoint>();
            if (wp != null)
            {
                if (movingRoutine != null) StopCoroutine(movingRoutine);
                movingRoutine = StartCoroutine(MoveToWaypoint(wp));
            }
        }
    }

    IEnumerator MoveToWaypoint(Waypoint target)
    {
        //Rotación objetivo: usa el Y del waypoint para orientar al jugador al llegar
        Quaternion targetRot = Quaternion.Euler(0f, target.transform.eulerAngles.y, 0f);

        //Destino a la misma altura del CharacterController
        Vector3 dest = target.transform.position;
        dest.y = transform.position.y;

        //Mover hasta estar dentro de stopDistance
        while (Vector3.Distance(transform.position, dest) > stopDistance)
        {
            //Suaviza giro
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);

            //Desplazamiento con CharacterController
            Vector3 dir = (dest - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);

            yield return null;
        }

        //Ajuste final
        transform.position = dest;
        transform.rotation = targetRot;
        current = target;
        movingRoutine = null;
    }

    ///Evita clicks a través de la UI (botones, etc.)
    bool IsPointerOverUI()
    {
        if (EventSystem.current == null) return false;

        if (EventSystem.current.IsPointerOverGameObject()) return true;

        for (int i = 0; i < Input.touchCount; i++)
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId))
                return true;

        return false;
    }

    //Depuración
    void DebugRayFromCenter()
    {
        if (Camera.main == null) { Debug.Log("No MainCamera"); return; }

        Vector3 center = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(center);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, waypointLayer, QueryTriggerInteraction.Collide))
            Debug.Log($"[Ray] Hit: {hit.collider.name} | Layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)}");
        else
            Debug.Log("[Ray] No golpeó ningún Waypoint");
    }
}
