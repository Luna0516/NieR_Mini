using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    float moveSpeed = 3.0f;

    Vector3 moveDir;

    Vector3 nextPos;

    Rigidbody rigid;

    PlayerInputActions inputActions;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // <Fuc> ==========================================================================

    private void Move()
    {
        nextPos = Time.fixedDeltaTime * moveSpeed * moveDir;

        rigid.MovePosition(rigid.position + nextPos);
    }

    // <InputAction> ==========================================================================

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputDir = context.ReadValue<Vector2>();

        moveDir.x = inputDir.x;
        moveDir.y = 0;
        moveDir.z = inputDir.y;
    }
}
