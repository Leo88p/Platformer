using UnityEngine;

public class Crystal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.InvokeCrystalPicked(transform.position, other.transform.rotation);
            Destroy(gameObject);
        }
    }
}
