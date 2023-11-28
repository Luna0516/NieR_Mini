using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyD : EnemyBase
{
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// 이동 방향
    /// </summary>
    private Vector3 moveDir;

    /// <summary>
    /// 목표 트랜스폼
    /// </summary>
    Transform target;

    /// <summary>
    /// 총알 발사 위치
    /// </summary>
    Transform firePos;

    // 컴포넌트
    Renderer[] renderers;

    // < > ========================================================================================

    protected override void Start()
    {
        base.Start();

        target = GameManager.Inst.Player.transform;
    }

    private void Update()
    {
        transform.LookAt(target);

        elapsedTIme += Time.deltaTime;

        // 업데이트 vs 코루틴 => 일단 업데이트에서 해보았다.
        if (elapsedTIme > fireDelay)
        {
            Fire();
            elapsedTIme = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        moveDir = (target.position - transform.position).normalized;
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * moveSpeed * moveDir);
    }

    // <Fuc>    ===================================================================================

    protected override void Init_Awake()
    {
        base.Init_Awake();

        renderers = GetComponentsInChildren<Renderer>();

        firePos = transform.GetChild(2);

        moveSpeed = 3.0f;
    }

    protected override void Fire()
    {
        Vector3 targetPos = target.position;
        targetPos.y = 0;

        Vector3 fireDir = targetPos - transform.position;

        fireDir.Normalize();

        // 자신의 총알 종류에 따라 앞으로 발사하는 총알 종류 변경
        switch (bulletType)
        {
            case EnemyBulletType.Normal:
                poolManager.GetEnemyBullet(fireDir, firePos.position);
                break;
            case EnemyBulletType.Invincible:
                poolManager.GetEnemyInvincibleBullet(fireDir, firePos.position);
                break;
            default:
                break;
        }
    }

    protected override void Die()
    {
        base.Die();

        gameObject.SetActive(false);
    }

    protected override IEnumerator OnHitEffect()
    {
        foreach (Renderer render in renderers)
        {
            render.material = materials[1];
        }

        yield return new WaitForSeconds(0.1f);

        foreach (Renderer render in renderers)
        {
            render.material = materials[0];
        }
    }
}
