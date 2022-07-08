using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float acceleration = 6f;
    [SerializeField] private float deceleration = 12f;

    [HideInInspector]
    public PlayerInputProvider playerInput;

    private CharacterController controller;
    private float currentSpeed;
    private Vector3 moveDirection;

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

        if(playerInput.moveInput != Vector2.zero)
        {
            moveDirection = new Vector3(playerInput.moveInput.x, 0, playerInput.moveInput.y);

            currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, deceleration * Time.deltaTime);
        }

        transform.forward = moveDirection;

        controller.Move(moveDirection * currentSpeed * Time.deltaTime);
    }

}
