using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody playerRigidbody;
    [SerializeField]
    private Transform playerCamera;
    [SerializeField]
    private float moveSpeed = 30f;
    private Rigidbody PlayerRigidbody
    {
        get 
        {
            if (playerRigidbody == null) playerRigidbody = GetComponentInChildren<Rigidbody>();

            return playerRigidbody;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 cameraDirection = new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z).normalized;
        Vector3 strafe = Quaternion.Euler(0, 90, 0) * cameraDirection;
        
        Vector3 direction = cameraDirection * Input.GetAxis("Vertical");
        strafe *= Input.GetAxis("Horizontal");

        direction = direction + strafe;

        playerRigidbody.velocity = direction * Time.deltaTime * moveSpeed;
    }
}