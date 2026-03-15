using UnityEngine;
using System;
using System.Collections.Generic;
public class DataModels : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Serializable]
    public class TranslationItem
    {
        public string languageCode; // например "ru", "en"
        public string text;
    }

    [Serializable]
    public class LocalizedTextData
    {
        public string id; // ”никальный ключ дл€ объекта в сцене
        public List<TranslationItem> translations;
    }

    [Serializable]
    public class LocalizationFile
    {
        public List<LocalizedTextData> items;
    }
}
