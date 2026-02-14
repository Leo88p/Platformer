using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public TMP_Text scoreMessagePrefab;
    private int chainedCrystalsCount = 0;
    private float timeSinceLastCrystal = 1;
    public bool Green_Key = false;
    public bool Yellow_Key = false;
    public bool Blue_Key = false;

    void Start()
    {
        EventManager.OnCrystalPicked += PickCrystal;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastCrystal += Time.deltaTime;
        if (timeSinceLastCrystal > 1)
        {
            chainedCrystalsCount = 0;
        }
    }

    void PickCrystal(Vector3 position, Quaternion rotation)
    {
        if (timeSinceLastCrystal < 1)
        {
            chainedCrystalsCount++;
        }
        timeSinceLastCrystal = 0;
        int scorePerCrystal = 10 + chainedCrystalsCount;
        score += scorePerCrystal;
        Instantiate(scoreMessagePrefab, position, rotation).text = $"+{scorePerCrystal}";
    }
}
