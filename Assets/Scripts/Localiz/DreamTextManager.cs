using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[System.Serializable]
public class LanguageText
{
    public string lang;
    public string content;
}

[System.Serializable]
public class LocationData
{
    public string locationId;
    public List<LanguageText> texts;
}

[System.Serializable]
public class DreamTextDatabase
{
    public List<LocationData> locations;
}



public class DreamTextManager : MonoBehaviour
{
    
    public static DreamTextManager Instance { get; private set; }

    [Header("Настройки JSON")]
    [Tooltip("Имя файла JSON в папке Resources (без расширения .json)")]
    public string jsonFileName = "Localization";

    [Header("Настройки Языка")]
    [Tooltip("Язык по умолчанию, если в PlayerPrefs нет сохранения")]
    public string defaultLanguage = "ru";

   
    private DreamTextDatabase _database;
    private string _currentLang;
    private bool _isInitialized = false;

    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }
        Instance = this;

       

        Initialize();
    }

   
    public void Initialize()
    {
        if (_isInitialized) return;

        // 1. Загрузка JSON
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);
        if (jsonFile != null)
        {
            _database = JsonUtility.FromJson<DreamTextDatabase>(jsonFile.text);
            Debug.Log($"[DreamTextManager] База загружена: {jsonFileName}.json");
        }
        else
        {
            Debug.LogError($"[DreamTextManager] Файл '{jsonFileName}.json' не найден в папке Resources!");
            // Создаем пустую базу, чтобы игра не крашилась
            _database = new DreamTextDatabase { locations = new List<LocationData>() };
        }

        // 2. Определение языка
        _currentLang = PlayerPrefs.GetString("GameLanguage", defaultLanguage);

        _isInitialized = true;
    }

   
    public void SetLanguage(string lang)
    {
        _currentLang = lang;
        PlayerPrefs.SetString("GameLanguage", lang);
        PlayerPrefs.Save();
        Debug.Log($"[DreamTextManager] Язык изменен на: {lang}");

       
    }

    
    public string GetCurrentLanguage()
    {
        return _currentLang;
    }

    
    public string GetText(string locationId)
    {
        if (!_isInitialized) Initialize();
        if (_database == null || _database.locations == null)
            return "Error: No Database";

        var location = _database.locations.FirstOrDefault(l => l.locationId == locationId);

        if (location == null)
        {
            Debug.LogWarning($"[DreamTextManager] ID '{locationId}' не найден в JSON!");
            return $"Error: ID '{locationId}' not found";
        }

        var textData = location.texts.FirstOrDefault(t => t.lang == _currentLang);

        if (textData == null)
        {
            textData = location.texts.FirstOrDefault();
            Debug.LogWarning($"[DreamTextManager] Перевода на {_currentLang} для '{locationId}' нет. Использован запасной вариант.");
        }

        return textData?.content ?? "Empty Text";
    }
}