using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CloudTriggerArea : MonoBehaviour
{
    [SerializeField, HideInInspector]
    private Collider triggerArea;

    public event UnityAction OnPlayerEnter;
    public event UnityAction OnPlayerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            OnPlayerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            OnPlayerExit?.Invoke();
        }
    }

    private void OnValidate()
    {
        if (triggerArea == null) 
        {
            triggerArea = GetComponent<Collider>();
        }

        if (!triggerArea.isTrigger)
        {
            triggerArea.isTrigger = true;
        }
    }
}