using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class StreetPlayer : MonoBehaviour
{
    [Header("References")]
    public Transform cameraPivot;
    public LayerMask waypointLayer;

    [Header("Movement")]
    public float moveSpeed = 4f;
    public float rotateSpeed = 6f;
    public float stopDistance = 0.03f;

    [Header("Panorama Spheres")]
    public GameObject[] panoramaSpheres; // Array de 3 esferas
    public float cameraMoveOffset = 0.3f;

    private CharacterController cc;
    private Coroutine movingRoutine;
    private Waypoint current;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        if (Camera.main == null)
            Debug.LogWarning("No hay cámara con tag 'MainCamera' en la escena.");
    }

    void Update()
    {
        if (!IsPointerOverUI())
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
                TrySetDestinationFromCenter();
        }

        if (Input.GetKeyDown(KeyCode.F1))
            DebugRayFromCenter();
    }

    void TrySetDestinationFromCenter()
    {
        if (Camera.main == null) return;

        Vector3 center = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(center);

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
        Quaternion targetRot = Quaternion.Euler(0f, target.transform.eulerAngles.y, 0f);
        Vector3 dest = target.transform.position;
        dest.y = transform.position.y;

        Vector3 startCamPos = cameraPivot.localPosition;
        Vector3 camOffset = new Vector3(0f, 0f, cameraMoveOffset);

        while (Vector3.Distance(transform.position, dest) > stopDistance)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
            Vector3 dir = (dest - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);

            cameraPivot.localPosition = Vector3.Lerp(cameraPivot.localPosition, startCamPos + camOffset, 0.05f);

            yield return null;
        }

        // Ajuste final
        transform.position = dest;
        transform.rotation = targetRot;
        cameraPivot.localPosition = startCamPos;
        current = target;
        movingRoutine = null;

        // Encender la esfera correspondiente al waypoint
        if (panoramaSpheres != null && panoramaSpheres.Length > 0 && target != null)
        {
            int activeIndex = Mathf.Clamp(target.activeSphereIndex, 0, panoramaSpheres.Length - 1);

            for (int i = 0; i < panoramaSpheres.Length; i++)
            {
                if (panoramaSpheres[i] != null)
                    panoramaSpheres[i].SetActive(i == activeIndex);
            }
        }
    }

    bool IsPointerOverUI()
    {
        if (EventSystem.current == null) return false;

        if (EventSystem.current.IsPointerOverGameObject()) return true;

        for (int i = 0; i < Input.touchCount; i++)
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId))
                return true;

        return false;
    }

    void DebugRayFromCenter()
    {
        if (Camera.main == null) return;

        Vector3 center = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(center);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, waypointLayer, QueryTriggerInteraction.Collide))
            Debug.Log($"[Ray] Hit: {hit.collider.name}");
        else
            Debug.Log("[Ray] No golpeó ningún Waypoint");
    }
}
