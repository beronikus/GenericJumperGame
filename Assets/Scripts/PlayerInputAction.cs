using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputAction : MonoBehaviour
{
    public static PlayerInputAction Instance { get; private set; }

    private InputActionsP inputAction;

    private void Awake()
    {
        Instance = this;
        inputAction = new InputActionsP();
        inputAction.Enable();

    }
    
    private void OnDisable()
    {
        inputAction.Disable();
    }

    public Vector2 MoveActionPressed()
    {
        return inputAction.Player.Move.ReadValue<Vector2>();
    }

    public bool JumpActionPresssed()
    {
        return inputAction.Player.Jump.IsPressed();
    }

    
    
}
