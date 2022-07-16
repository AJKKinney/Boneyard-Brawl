using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputProvider : MonoBehaviour
{
    [HideInInspector]
    public Vector2 moveInput;
    [HideInInspector]
    public PlayerCharacterController playerController;
    public InteractManager playerInteraction;

    public void OnMove(InputAction.CallbackContext ctx) => moveInput = ctx.ReadValue<Vector2>();

    public void OnDodge(InputAction.CallbackContext ctx) => playerController.Dodge(ctx.ReadValue<float>());

    public void OnInteract(InputAction.CallbackContext ctx) => playerInteraction.Interact(ctx.ReadValue<float>());
}
