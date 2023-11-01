using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    /// <summary>
    /// 총알의 이동 속도
    /// </summary>
    float bulletSpeed = 15.0f;

    /// <summary>
    /// 총알의 생성되어 있는 시간
    /// </summary>
    float lifeTime = 1.0f;

    /// <summary>
    /// 이동 방향
    /// </summary>
    Vector3 fireDir;

    /// <summary>
    /// 이동 방향 설정용 프로퍼티
    /// </summary>
    public Vector3 FireDir
    {
        set
        {
            if(fireDir != value)
            {
                fireDir = value;
                transform.forward = fireDir;
            }
        }
    }

    private void OnEnable()
    {
        StartCoroutine(LifeOver());
    }

    private void Update()
    {
        transform.position += Time.deltaTime * bulletSpeed * fireDir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet") || other.CompareTag("EnemyShield") || other.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 총알의 활성화 시간 코루틴
    /// </summary>
    private IEnumerator LifeOver()
    {
        yield return new WaitForSeconds(lifeTime);

        gameObject.SetActive(false);
    }
}
