using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    float moveSpeed = 3.0f;

    float mainTargetDistance = 100.0f;

    Vector3 moveDir;

    Vector3 nextPos;

    Vector3 lookVec;

    public Transform mainTarget;

    Rigidbody rigid;

    PlayerInputActions inputActions;

    private void Awake()
    {
        mainTarget = null;

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
        FindEnemy();
    }

    // <Fuc> ==========================================================================

    private void Move()
    {
        nextPos = Time.fixedDeltaTime * moveSpeed * moveDir;

        rigid.MovePosition(rigid.position + nextPos);
    }

    private void FindEnemy()
    {
        mainTargetDistance = 121.0f;
        mainTarget = null;

        // stay에서 하는 것도 생각 해 보 기
        // 현재는 Update에서 실행할 계획
        Collider[] targets = Physics.OverlapSphere(transform.position, 10.0f, 1 << 10);

        if (targets.Length > 0)
        {
            int forSize = targets.Length;

            for (int i = 0; i < forSize; i++)
            {
                float distance = Vector3.SqrMagnitude(targets[i].transform.position - transform.position);
                if (mainTargetDistance > distance)
                {
                    mainTargetDistance = distance;
                    mainTarget = targets[i].transform;
                }
            }

            if (mainTarget != null)
            {
                lookVec = mainTarget.transform.position - transform.position;
                lookVec.Normalize();
            }
        }
        else
        {
            lookVec = Vector3.forward;
        }


        transform.forward = Vector3.Lerp(transform.forward, lookVec, Time.fixedDeltaTime * 10.0f);
    }

    // <InputAction> ==========================================================================

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputDir = context.ReadValue<Vector2>();

        moveDir.x = inputDir.x;
        moveDir.y = 0;
        moveDir.z = inputDir.y;
    }

    // <TestCode> ==========================================================================

    public void Test_FindEnemy()
    {
        FindEnemy();
    }
}
