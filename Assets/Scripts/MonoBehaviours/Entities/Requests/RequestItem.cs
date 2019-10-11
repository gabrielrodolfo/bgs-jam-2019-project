using UnityEngine;
using UnityEngine.EventSystems;

public class RequestItem : MonoBehaviour
{
    [SerializeField, HideInInspector]
    private string itemName;
    [SerializeField]
    private Sprite representingImage;
    [SerializeField]
    private new Rigidbody rigidbody;

    public string ItemName { get => itemName; }
    public Sprite RepresentingImage { get => representingImage; }
    public Rigidbody Rigidbody { get => rigidbody; private set => rigidbody = value; }

    private void OnValidate()
    {
        itemName = gameObject.name;
        if (rigidbody == null) rigidbody = GetComponentInChildren<Rigidbody>();
    }
}