using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    public void OnLeft(InputValue value)
    {
        playerController.OnLeftInput(value.isPressed);
    }

    public void OnRight(InputValue value)
    {
        playerController.OnRightInput(value.isPressed);
    }
}
