using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : EnemyBase
{
    public System.Action onDie;

    private void Update()
    {
        elapsedTIme += Time.deltaTime;

        transform.Rotate(transform.up, Time.deltaTime * rotSpeed);

        if (elapsedTIme > fireDelay)
        {
            Fire();
            elapsedTIme = 0.0f;
        }
    }

    // <기타 함수> ====================================================================================

    protected override void Init_Awake()
    {
        rotSpeed = 30.0f;
        elapsedTIme = 0.0f;
        fireDelay = 0.75f;
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

        onDie?.Invoke();
    }
}
