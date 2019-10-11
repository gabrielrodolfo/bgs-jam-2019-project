using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody playerRigidbody;
    [SerializeField]
    private GameObject playerHand;
    private Animator playerAnimator;
    [SerializeField]
    private Transform playerCamera;
    [SerializeField]
    private float moveSpeed = 30f;

    private readonly int IS_WALKING_HASH = Animator.StringToHash("IsWalking");
    private readonly int ANIM_SPEED_HASH = Animator.StringToHash("AnimSpeed");


    private Rigidbody PlayerRigidbody
    {
        get 
        {
            if (playerRigidbody == null) playerRigidbody = GetComponentInChildren<Rigidbody>();

            return playerRigidbody;
        }
    }


    private void Start()
    {
        playerAnimator = playerHand.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        AnimatePlayer();
    }

    private void MovePlayer()
    {
        Vector3 cameraDirection = new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z).normalized;
        Vector3 strafe = Quaternion.Euler(0, 90, 0) * cameraDirection;
        
        Vector3 direction = cameraDirection * Input.GetAxis("Vertical");
        strafe *= Input.GetAxis("Horizontal");

        direction = Vector3.ClampMagnitude(direction + strafe, 1f);

        Vector3 horizontalVelocity = (direction * Time.deltaTime * moveSpeed);
        Vector3 generalVelocity = horizontalVelocity + new Vector3(0,playerRigidbody.velocity.y, 0);

        playerRigidbody.velocity = generalVelocity;
    }

    private void AnimatePlayer()
    {
        playerAnimator.SetBool(IS_WALKING_HASH, !PlayerRigidbody.velocity.Equals(Vector3.zero));
        playerAnimator.SetFloat(ANIM_SPEED_HASH, PlayerRigidbody.velocity.magnitude / 10f);
    }
}