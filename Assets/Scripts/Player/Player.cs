using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 총알 발사 가능 여부
    /// </summary>
    private bool isFireReady = false;

    private int bodySize = 3;

    /// <summary>
    /// 체력
    /// </summary>
    private int health = 3;

    public int Health
    {
        get => health;
        private set
        {
            if(health != value)
            {
                health = value;
                if (health > 0)
                {
                    SetBody();
                }
                else
                {
                    Die();
                }
            }
        }
    }

    /// <summary>
    /// 총알 발사 쿨타임용 시간
    /// </summary>
    private float elapsedTime = 0.0f;

    /// <summary>
    /// 이동 속도
    /// </summary>
    public float moveSpeed = 6.0f;

    /// <summary>
    /// 총알 발사 딜레이 시간
    /// </summary>
    public float fireDelay = 0.1f;

    /// <summary>
    /// 적을 찾는 범위
    /// </summary>
    public float findEnemyDistance = 10.0f;

    /// <summary>
    /// 움직일 방향
    /// </summary>
    private Vector3 moveDir;

    /// <summary>
    /// 이동 위치
    /// </summary>
    private Vector3 nextPos;

    /// <summary>
    /// 바라보는 방향
    /// </summary>
    private Vector3 lookVec;

    /// <summary>
    /// 적 레이어 마스크
    /// </summary>
    private LayerMask enemyLayer;

    /// <summary>
    /// 총알 발사 코루틴
    /// </summary>
    private IEnumerator fireCoru;

    /// <summary>
    /// 가장 가까운 적
    /// </summary>
    private Transform mainTarget;

    /// <summary>
    /// 총알 발사 위치
    /// </summary>
    private Transform firePos;

    /// <summary>
    /// 몸통의 오브젝트들
    /// </summary>
    private GameObject[] bodyObjects;

    /// <summary>
    /// 풀매니저
    /// </summary>
    private PoolManager poolManager;

    /// <summary>
    /// InputSystem
    /// </summary>
    private PlayerInputActions inputActions;

    // 컴포넌트
    private Rigidbody rigid;

    // < >  =======================================================================================

    private void Awake()
    {
        health = bodySize;

        enemyLayer = 1 << LayerMask.NameToLayer("Enemy");

        fireCoru = Fire();

        firePos = transform.GetChild(3);

        bodyObjects = new GameObject[3];

        for(int i = 0; i< bodySize; i++)
        {
            bodyObjects[i] = transform.GetChild(i).gameObject;
        }

        poolManager = PoolManager.Inst;

        rigid = GetComponent<Rigidbody>();

        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Fire.performed += OnFire;
        inputActions.Player.Fire.canceled += OnFire;
    }

    private void OnDisable()
    {
        inputActions.Player.Fire.canceled -= OnFire;
        inputActions.Player.Fire.performed -= OnFire;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime > fireDelay)
        {
            isFireReady = true;
        }

        FindEnemy();

        Rotate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Health--;
        }
    }


    // <Fuc>    ===================================================================================

    /// <summary>
    /// 이동 함수
    /// </summary>
    private void Move()
    {
        nextPos = Time.fixedDeltaTime * moveSpeed * moveDir;

        rigid.MovePosition(rigid.position + nextPos);
    }

    /// <summary>
    /// 회전 함수
    /// </summary>
    private void Rotate()
    {
        if (mainTarget)
        {
            lookVec = mainTarget.transform.position - transform.position;
        }
        else
        {
            lookVec = moveDir;
        }

        lookVec.Normalize();

        transform.forward = Vector3.Lerp(transform.forward, lookVec, Time.fixedDeltaTime * 30.0f);
    }

    /// <summary>
    /// 가까운 적을 찾는 함수 (Physics.OverlapSphere 사용)
    /// </summary>
    private void FindEnemy()
    {
        //메인 타겟은 좀더 좁은 범위에서 찾게하고, 총알은 원래 설정한 범위에서 찾게 하기
        float mainTargetDistance = (findEnemyDistance - 1) * (findEnemyDistance - 1);
        float bulletTargetDistance = (findEnemyDistance + 1) * (findEnemyDistance + 1);

        mainTarget = null;

        Transform bulletTarget = null;

        Collider[] targets = Physics.OverlapSphere(transform.position, findEnemyDistance, enemyLayer);

        if (targets.Length > 0)
        {
            int forSize = targets.Length;

            for (int i = 0; i < forSize; i++)
            {
                float distance = Vector3.SqrMagnitude(targets[i].transform.position - transform.position);

                if (targets[i].CompareTag("EnemyBullet"))
                {
                    if (bulletTargetDistance > distance)
                    {
                        bulletTargetDistance = distance;
                        bulletTarget = targets[i].transform;
                    }
                }
                else if (targets[i].CompareTag("Enemy"))
                {
                    if (mainTargetDistance > distance)
                    {
                        mainTargetDistance = distance;
                        mainTarget = targets[i].transform;
                    }
                }
            }

            if (!mainTarget)
            {
                mainTarget = bulletTarget;
            }
        }
    }

    /// <summary>
    /// 플레이어의 체력에 따라 몸통의 개수를 조절하는 함수
    /// </summary>
    private void SetBody()
    {
        for (int i = 0; i < health; i++)
        {
            bodyObjects[i].SetActive(true);
        }

        for(int i = bodySize - 1; i > health - 1; i--)
        {
            bodyObjects[i].SetActive(false);
        }
    }

    /// <summary>
    /// 플레이어가 죽었을 때 실행할 함수
    /// </summary>
    private void Die()
    {
        Debug.Log("죽음!!!!");
    }

    /// <summary>
    /// 총알 발사 코루틴
    /// </summary>
    private IEnumerator Fire()
    {
        while (true)
        {
            if (isFireReady)
            {
                elapsedTime = 0.0f;
                isFireReady = false;
                poolManager.GetPlayerBullet(transform.forward, firePos.position);
            }

            yield return null;
        }
    }


    // <InputAction>   ===============================================================================

    /// <summary>
    /// 이동 입력 함수
    /// </summary>
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputDir = context.ReadValue<Vector2>();

        moveDir.x = inputDir.x;
        moveDir.y = 0;
        moveDir.z = inputDir.y;
    }

    /// <summary>
    /// 총알 발사 입력 함수
    /// </summary>
    private void OnFire(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            StopCoroutine(fireCoru);
        }
        else
        {
            StartCoroutine(fireCoru);
        }
    }


    // <TestCode>   ================================================================================

    /// <summary>
    /// 적을 찾는 함수 테스트용
    /// </summary>
    public void Test_FindEnemy()
    {
        FindEnemy();
    }

    /// <summary>
    /// 플레이어 체력 변화 함수 테스트용
    /// </summary>
    /// <param name="health"> 변화시킬 값 (-1, 1) </param>
    public void Test_HP(int health)
    {
        Health += health;
    }
}