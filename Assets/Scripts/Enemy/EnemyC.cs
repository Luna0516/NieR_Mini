using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : EnemyBase
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

        moveSpeed = 3.0f;
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
