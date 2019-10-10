using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField]
    private uint id;

    public uint ID { get => id; }
}