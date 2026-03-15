using UnityEngine;

public class Blue_Door : MonoBehaviour
{
    public GameManager GameManager;

    [Header("Настройки открытия двери")]
    public bool useRotation = true;        // Открыть поворотом (как обычная дверь)
    public Vector3 openAngle = new Vector3(0, 90, 0);  // Угол поворота при открытии
    public float openDuration = 0.5f;      // Время анимации открытия в секундах
    public bool disableCollisionAfterOpen = true;  // Отключить коллизию после открытия
    public Items type;

    private bool isOpen = false;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float openTimer = 0f;

    private void Awake()
    {
        if (GameManager == null)
        {
            GameManager = FindFirstObjectByType<GameManager>();
            if (GameManager == null)
            {
                Debug.LogError($"[{gameObject.name}] GameManager не найден на сцене!");
            }
        }

        startRotation = transform.rotation;
        targetRotation = startRotation * Quaternion.Euler(openAngle);
    }

    private void OnCollisionEnter(Collision other)
    {
        // Принцип столкновения НЕ изменён: всё так же реагируем на OnCollisionEnter
        if (other.gameObject.CompareTag("Player") && GameManager != null && GameManager.inventory[type] > 0 && !isOpen)
        {
            GameManager.OpenDoor(type, transform.position, other.transform.rotation);
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        isOpen = true;

        // Если нужно мгновенно открыть без анимации:
        if (openDuration <= 0f)
        {
            ApplyDoorOpen(1f);
            return;
        }

        // Иначе запускаем плавную анимацию
        openTimer = 0f;
    }

    private void Update()
    {
        if (isOpen && openDuration > 0f && openTimer < openDuration)
        {
            openTimer += Time.deltaTime;
            float t = Mathf.Clamp01(openTimer / openDuration);
            ApplyDoorOpen(t);
        }
    }

    private void ApplyDoorOpen(float progress)
    {
        // Плавная интерполяция поворота
        if (useRotation)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, progress);
        }
        // При необходимости можно добавить открытие сдвигом:
        // else { transform.position = Vector3.Lerp(startPosition, targetPosition, progress); }

        // Когда анимация завершена — отключаем коллизию, чтобы игрок мог пройти
        if (progress >= 1f && disableCollisionAfterOpen)
        {
            var colliders = GetComponents<Collider>();
            foreach (var col in colliders)
            {
                col.enabled = false;
            }
        }
    }
}