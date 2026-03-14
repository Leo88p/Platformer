using UnityEngine;

public class Key_script : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            if (GameManager != null)
            {
                GameManager.Blue_Key = true;
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError($"[{gameObject.name}] Попытка собрать ключ, но GameManager = null!");
            }
        }
    }
}