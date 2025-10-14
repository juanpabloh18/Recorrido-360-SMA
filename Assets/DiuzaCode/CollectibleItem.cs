using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [Header("Configuración del objeto")]
    public string itemID;                     // ID único del objeto
    public bool startHidden = true;           // Estado inicial (oculto/visible)
    public float detectionRadius = 2f;        // Radio para detectar al jugador

    private bool isCollected = false;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.SetActive(!startHidden);
    }

    void Update()
    {
        if (isCollected || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRadius && Input.GetKeyDown(KeyCode.E))
            Collect();
    }

    void Collect()
    {
        isCollected = true;
        gameObject.SetActive(false);
        PlayerPrefs.SetInt(itemID, 1); // Guardar que fue recogido
        PlayerPrefs.Save();
        CheckCollectionProgress();
    }

    void CheckCollectionProgress()
    {
        int totalCollected = 0;
        string[] ids = { "item1", "item2", "item3" }; // IDs de colección ejemplo

        foreach (var id in ids)
            if (PlayerPrefs.GetInt(id, 0) == 1)
                totalCollected++;

        if (totalCollected == ids.Length)
            Debug.Log("🎉 ¡Colección completada! Recompensa desbloqueada.");
    }
}
