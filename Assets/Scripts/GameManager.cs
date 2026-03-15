using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public TMP_Text scoreMessagePrefab;
    private int chainedCrystalsCount = 0;
    private float timeSinceLastCrystal = 1;
    public Dictionary<Items, int> inventory = System.Enum.GetValues(typeof(Items)).Cast<Items>().ToDictionary(i => i, i => 0);

    // Update is called once per frame
    void Update()
    {
        timeSinceLastCrystal += Time.deltaTime;
        if (timeSinceLastCrystal > 1)
        {
            chainedCrystalsCount = 0;
        }
    }

    public void PickCrystal(Vector3 position, Quaternion rotation)
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
    public void PickItem(Items name, int scorePerItem, Vector3 position, Quaternion rotation)
    {
        inventory[name]++;
        score += scorePerItem;
        Instantiate(scoreMessagePrefab, position, rotation).text = $"+{scorePerItem}";
    }
    public void OpenDoor(Items name, Vector3 position, Quaternion rotation)
    {
        inventory[name]--;
        int scorePerDoor = 50;
        score += scorePerDoor;
        Instantiate(scoreMessagePrefab, position, rotation).text = $"+{scorePerDoor}";
    }
}
