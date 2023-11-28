using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : EnemyBase
{
    /// <summary>
    /// 회전 속도
    /// </summary>
    public float rotSpeed = 30.0f;

    // < > ========================================================================================

    private void Update()
    {
        elapsedTIme += Time.deltaTime;

        transform.Rotate(transform.up, Time.deltaTime * rotSpeed);

        // 업데이트 vs 코루틴 => 일단 업데이트에서 해보았다.
        if (elapsedTIme > fireDelay)
        {
            Fire();
            elapsedTIme = 0.0f;
        }
    }

    // <Fuc > ====================================================================================

    protected override void Init_Awake()
    {
        base.Init_Awake();

        fireDelay = 0.75f;
    }

    protected override void Fire()
    {
        // 총알 타입에 맞춰서 자신의 현재의 up, down, left, right 방향으로 총알 생성해서 날리기
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

        gameObject.SetActive(false);
    }
}
