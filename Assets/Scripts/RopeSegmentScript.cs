using UnityEngine;

public class RopeSegmentScript : MonoBehaviour
{
    void OnTriggerEnterOrStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.InvokeRopeTriggered();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterOrStay(other);
    }
    void OnTriggerStay(Collider other)
    {
        OnTriggerEnterOrStay(other);
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.InvokeRopeExit();
        }
    }
}
