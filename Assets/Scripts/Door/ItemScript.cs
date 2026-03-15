using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public Items ItemType;
    public int scorePerItem;
    GameManager _gameManager;
    void Awake()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gameManager.PickItem(ItemType, scorePerItem, transform.position, other.transform.rotation);
            Destroy(gameObject);
        }
    }
}