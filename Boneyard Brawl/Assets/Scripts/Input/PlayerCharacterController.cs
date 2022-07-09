using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float ghostMoveSpeed = 4f;
    [SerializeField] private float skeletonMoveSpeed = 2.5f;
    [SerializeField] private float humanMoveSpeed = 2f;
    [SerializeField] private float acceleration = 6f;
    [SerializeField] private float deceleration = 12f;

    [Header("Player GFX")]
    [SerializeField] private GameObject ghostGFX;
    [SerializeField] private GameObject skeletonGFX;
    [SerializeField] private GameObject humanGFX;

    [HideInInspector]
    public PlayerInputProvider playerInput;
    [HideInInspector]
    public bool movementLocked = false;

    private CharacterController controller;
    private float currentSpeed;
    private Vector3 moveDirection;
    [SerializeField] private PlayerForm currentForm = PlayerForm.Skeleton;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInput == null)
        {
            return;
        }

        if (movementLocked == false)
        {
            CalculateMove();

            controller.Move(moveDirection * currentSpeed * Time.deltaTime);
        }
        else
        {
            currentSpeed = 0f;
        }
    }

    //calculate move direction and speed
    private void CalculateMove()
    {
        float targetSpeed = 0;

        if (playerInput.moveInput != Vector2.zero)
        {
            moveDirection = new Vector3(playerInput.moveInput.x, 0, playerInput.moveInput.y);

            if (currentForm == PlayerForm.Ghost)
            {
                targetSpeed = ghostMoveSpeed;
            }
            else if (currentForm == PlayerForm.Skeleton)
            {
                targetSpeed = skeletonMoveSpeed;
            }
            else if (currentForm == PlayerForm.Human)
            {
                targetSpeed = humanMoveSpeed;
            }
        }

        if (currentSpeed <= targetSpeed)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        }
        else if (currentSpeed > targetSpeed)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, deceleration * Time.deltaTime);
        }

        transform.forward = moveDirection;
    }

    //dodge
    public void Dodge()
    {
        
    }

    //changes player form negatively
    public void TakeHit()
    {
        if(currentForm == PlayerForm.Dead)
        {
            return;
        }

        currentForm -= 1;

        if (currentForm == PlayerForm.Dead)
        {
            this.gameObject.SetActive(false);
        }
        else if(currentForm == PlayerForm.Ghost)
        {
            ghostGFX.SetActive(true);
            skeletonGFX.SetActive(false);
        }
        else if(currentForm == PlayerForm.Skeleton)
        {
            skeletonGFX.SetActive(true);
            humanGFX.SetActive(false);
        }
    }

    //changes players form positively
    public void GainHumanity()
    {
        if (currentForm == PlayerForm.Human)
        {
            return;
        }

        currentForm += 1;

        if (currentForm == PlayerForm.Skeleton)
        {
            skeletonGFX.SetActive(true);
            ghostGFX.SetActive(false);
        }
        else if (currentForm == PlayerForm.Human)
        {
            humanGFX.SetActive(true);
            skeletonGFX.SetActive(false);
        }
    }

    public enum PlayerForm
    { 
        Dead,
        Ghost,
        Skeleton,
        Human
    }
}
