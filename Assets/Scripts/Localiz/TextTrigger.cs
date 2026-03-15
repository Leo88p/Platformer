using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class TextTrigger : MonoBehaviour
{
    [Header("Настройки")]
    [Tooltip("Уникальный ID, который совпадает с JSON")]
    public string locationId = "level1_intro_sign";

    [Tooltip("Как долго текст будет виден (сек)")]
    public float displayDuration = 5f;

    [Tooltip("Скорость исчезновения (сек)")]
    public float fadeDuration = 1f;

    [Header("Ссылки на UI")]
    [Tooltip("Панель с текстом (должна иметь CanvasGroup и TMP_Text)")]
    public GameObject textPanel;

    private CanvasGroup _canvasGroup;
    private TMP_Text _textComponent;
    private Coroutine _displayCoroutine;

    private void Awake()
    {
       
        if (textPanel != null)
        {
            _canvasGroup = textPanel.GetComponent<CanvasGroup>();
            _textComponent = textPanel.GetComponentInChildren<TMP_Text>();

            if (_canvasGroup == null)
            {
                _canvasGroup = textPanel.AddComponent<CanvasGroup>();
            }

           
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
        else
        {
            Debug.LogError("TextTrigger: Не назначена текстовая панель (Text Panel)!");
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowText();
        }
    }

    private void ShowText()
    {
       
        if (DreamTextManager.Instance == null)
        {
            Debug.LogError("TextTrigger: DreamTextManager всё ещё не найден! Проверьте сцену.");
            return;
        }

        if (_textComponent == null) return;

        if (_displayCoroutine != null)
        {
            StopCoroutine(_displayCoroutine);
        }

        string textContent = DreamTextManager.Instance.GetText(locationId);
        _textComponent.text = textContent;

        _displayCoroutine = StartCoroutine(DisplayRoutine());
    }

    private IEnumerator DisplayRoutine()
    {
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Clamp01(timer / fadeDuration);
            yield return null;
        }
        _canvasGroup.alpha = 1;

        yield return new WaitForSeconds(displayDuration);

        timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            yield return null;
        }
        _canvasGroup.alpha = 0;

        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}