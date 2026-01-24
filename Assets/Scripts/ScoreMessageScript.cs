using TMPro;
using UnityEngine;

public class ScoreMessageScript : MonoBehaviour
{
    private TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = text.color;
        color.a -= Time.deltaTime;
        text.color = color;
        if (color.a <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
