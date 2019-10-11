using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class RequestItemDropArea : MonoBehaviour
{
    [SerializeField, HideInInspector]
    private Collider dropArea;

    public event UnityAction<RequestItem> OnItemReceived;

    private void OnTriggerEnter(Collider other)
    {
        RequestItem ri = other.GetComponentInParent<RequestItem>();
        if (ri == null) ri = other.GetComponentInChildren<RequestItem>();
        
        if (ri != null)
        {
            OnItemReceived?.Invoke(ri);
        }
    }

    private void OnValidate()
    {
        if (dropArea == null)
        {
            dropArea = GetComponent<Collider>();
        }

        if (!dropArea.isTrigger)
        {
            dropArea.isTrigger = true;
        }
    }   
}