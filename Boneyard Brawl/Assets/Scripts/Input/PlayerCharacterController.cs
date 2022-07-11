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
    [SerializeField] private PlayerForm currentForm = PlayerForm.Skeleton;

    [HideInInspector] public PlayerInputProvider playerInput;
    [HideInInspector] public bool movementLocked = false;

    private CharacterController controller;
    private float currentSpeed;
    private Vector3 moveDirection;

    [Header ("Player Dodge")]
    [SerializeField] private float dodgeTimerLength = 0.05f;
    [SerializeField] private float dodgeMoveLockLength = 0.15f;
    [SerializeField] private float dodgeCooldownLength = 0.15f;
    [SerializeField] private float dodgeDeceleration = 15f;

    private bool isDodging = false;
    private float dodgeTimer = 0f;

    [Header("Player GFX")]
    [SerializeField] private GameObject ghostGFX;
    [SerializeField] private GameObject skeletonGFX;
    [SerializeField] private GameObject humanGFX;



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
        else if (isDodging == true)
        {
            CalculateDodge();

            controller.Move(moveDirection * currentSpeed * Time.deltaTime);
        }
        else
        {
            currentSpeed = 0f;
        }

        RunCooldowns();
    }

    //calculate move direction and speed
    private void CalculateMove()
    {
        float targetSpeed = 0f;

        //normal movement
        if (dodgeTimer <= 0)
        {
            if (playerInput.moveInput != Vector2.zero)
            {
                moveDirection = new Vector3(playerInput.moveInput.x, 0, playerInput.moveInput.y);

                targetSpeed = CalculateFormSpeed();
            }

            if (currentSpeed <= targetSpeed)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
            }
            else
            {
               currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, deceleration * Time.deltaTime);
            }
        }
        //dodge Move
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, dodgeDeceleration * Time.deltaTime);
        }

        transform.forward = moveDirection;
    }

    //initiate dodge
    //takes in the buttons float value 0 = release, 1 = press
    public void Dodge(float buttonValue)
    {
        if (dodgeTimer <= 0f && buttonValue == 1f)
        {
            movementLocked = true;
            isDodging = true;
            dodgeTimer = dodgeTimerLength + dodgeMoveLockLength + dodgeCooldownLength;
        }
    }

    //calculate dodge direction and speed
    private void CalculateDodge()
    {
        float targetSpeed = 0f;
        float dodgeMultiplier = 20f;

        targetSpeed = CalculateFormSpeed();

        //set dodge speed, return to normal speed if cooldown is active
        if (dodgeTimer > dodgeCooldownLength + dodgeMoveLockLength)
        {
            targetSpeed *= dodgeMultiplier;
        }

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        //Catch Edge Case: Player Hasn't Moved
        if (moveDirection == Vector3.zero)
        {
            transform.forward = moveDirection;
        }
    }

    //calculate speed based on current form
    private float CalculateFormSpeed()
    {
        float targetSpeed = 0;

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

        return targetSpeed;
    }

    //changes player form negatively
    public void LoseHumanity()
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

    //Runs dodge cooldown
    private void DodgeCooldown()
    {
        if(dodgeTimer <= 0f)
        {
            return;
        }

        dodgeTimer -= Time.deltaTime;

        //resume movement capabilities at end of cooldown
        if (dodgeTimer <= dodgeCooldownLength)
        {
            movementLocked = false;
            isDodging = false;
        }
    }

    private void RunCooldowns()
    {
        DodgeCooldown();
    }
}
