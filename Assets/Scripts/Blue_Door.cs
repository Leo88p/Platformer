using UnityEngine;

public class Blue_Door : MonoBehaviour
{
    public GameManager GameManager;

    private void Awake()
    {
        // Если не назначен в Инспекторе — ищем на сцене
        if (GameManager == null)
        {
            GameManager = FindObjectOfType<GameManager>();
            if (GameManager == null)
            {
                Debug.LogError($"[{gameObject.name}] GameManager не найден на сцене!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && (GameManager.Blue_Key == true))
        {
                Destroy(gameObject);
        }
    }
}
