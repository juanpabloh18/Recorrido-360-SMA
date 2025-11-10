using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Refs")]
    public Camera cam;

    [Header("Look")]
    public float sensitivity = 0.15f; //De píxeles a grados
    public float minPitch = -85f;
    public float maxPitch = 85f;
    public bool lockCursorOnPlay = true;

    [Header("Zoom (FOV)")]
    public float minFov = 30f;
    public float maxFov = 90f;
    public float zoomSpeed = 5f;

    float yaw;   //Rotación horizontal (Y) en el padre
    float pitch; //Rotación vertical (X) en este pivot
    Transform yawParent;

    void Awake()
    {
        if (cam == null) cam = Camera.main;
        yawParent = transform.parent != null ? transform.parent : transform;
        Vector3 e = yawParent.eulerAngles;
        yaw = e.y;
        pitch = transform.localEulerAngles.x;
    }

    void Start()
    {
        if (lockCursorOnPlay) { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }
    }

    void Update()
    {
        HandleLook();
        HandleZoom();
    }

    void HandleLook()
    {
        Vector2 delta = Vector2.zero;

        //Touch para movimiento (botón izquierdo o derecho por ahora)
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            delta = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));

        // Touch (un dedo)
        if (Input.touchCount == 1 && !Input.GetMouseButton(0))
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Moved) delta = t.deltaPosition / 5f;
        }

        yaw   += delta.x * sensitivity * 10f;
        pitch += delta.y * sensitivity * 10f;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        yawParent.rotation = Quaternion.Euler(0f, yaw, 0f);
        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void HandleZoom()
    {
        float scroll = Input.mouseScrollDelta.y;

        //Pinch zoom (dos dedos)
        if (Input.touchCount == 2)
        {
            var t0 = Input.GetTouch(0);
            var t1 = Input.GetTouch(1);
            var prev0 = t0.position - t0.deltaPosition;
            var prev1 = t1.position - t1.deltaPosition;
            float prevMag = (prev0 - prev1).magnitude;
            float currMag = (t0.position - t1.position).magnitude;
            float diff = currMag - prevMag;
            scroll = diff * 0.01f; //Sensibilidad móvil
        }

        if (Mathf.Abs(scroll) > 0.0001f)
        {
            float target = Mathf.Clamp(cam.fieldOfView - scroll * zoomSpeed, minFov, maxFov);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, target, 0.35f);
        }
    }
}
