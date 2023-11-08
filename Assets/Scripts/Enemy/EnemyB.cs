using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : EnemyBase
{
    /// <summary>
    /// 회전 속도
    /// </summary>
    public float rotSpeed = 40.0f;

    /// <summary>
    /// 부모 트랜스폼
    /// </summary>
    Transform parentTr;

    // < >  =======================================================================================

    private void Update()
    {
        elapsedTIme += Time.deltaTime;

        parentTr.Rotate(transform.up, Time.deltaTime * rotSpeed);

        if (elapsedTIme > fireDelay)
        {
            Fire();
            elapsedTIme = 0.0f;
        }
    }

    // <Fuc>    ===================================================================================

    protected override void Init_Awake()
    {
        base.Init_Awake();

        fireDelay = 1.5f;

        parentTr = transform.parent;
    }

    protected override void Fire()
    {
        switch (bulletType)
        {
            case EnemyBulletType.Normal:
                poolManager.GetEnemyBullet(transform.forward, transform.position);
                poolManager.GetEnemyBullet(-transform.forward, transform.position);
                poolManager.GetEnemyBullet(transform.right, transform.position);
                poolManager.GetEnemyBullet(-transform.right, transform.position);
                break;
            case EnemyBulletType.Invincible:
                poolManager.GetEnemyInvincibleBullet(transform.forward, transform.position);
                poolManager.GetEnemyInvincibleBullet(-transform.forward, transform.position);
                poolManager.GetEnemyInvincibleBullet(transform.right, transform.position);
                poolManager.GetEnemyInvincibleBullet(-transform.right, transform.position);
                break;
            case EnemyBulletType.None: 
            default:
                break;
        }
    }

    protected override void Die()
    {
        base.Die();

        parentTr.gameObject.SetActive(false);
    }
}
