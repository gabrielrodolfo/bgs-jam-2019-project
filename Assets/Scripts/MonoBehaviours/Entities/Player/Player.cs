using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;
    
    public Camera PlayerCamera { get => playerCamera; }

}