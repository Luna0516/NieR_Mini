using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적 총알의 종류
/// </summary>
public enum EnemyBulletType
{
    None,
    Normal,
    Invincible
}

public class EnemyBullet : MonoBehaviour
{
    /// <summary>
    /// 총알의 이동 속도
    /// </summary>
    public float moveSpeed = 5.0f;

    /// <summary>
    /// 자신의 총알 종류
    /// </summary>
    public EnemyBulletType type = EnemyBulletType.None;

    private void Update()
    {
        transform.position += Time.deltaTime * moveSpeed * transform.forward;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }

        switch (type)
        {
            case EnemyBulletType.Normal:
                if (collision.gameObject.CompareTag("PlayerBullet"))
                {
                    gameObject.SetActive(false);
                }
                break;
            case EnemyBulletType.None:
            case EnemyBulletType.Invincible:
            default:
                break;
        }
        
    }
}
