using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Tooltip("Opcional: otros waypoints accesibles desde este punto")]
    public Waypoint[] neighbors;

    [Header("Panorama Sphere")]
    [Tooltip("Índice de la esfera que se activa al llegar a este waypoint")]
    public int activeSphereIndex = 0; // 0 = primera esfera, 1 = segunda, 2 = tercera

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
        if (neighbors == null) return;
        Gizmos.color = new Color(0f, 1f, 1f, 0.35f);
        foreach (var n in neighbors)
            if (n) Gizmos.DrawLine(transform.position, n.transform.position);
    }
}
