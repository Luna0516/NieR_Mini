using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    /// <summary>
    /// 총알의 이동 속도
    /// </summary>
    public float bulletSpeed = 15.0f;

    /// <summary>
    /// 총알의 생성되어 있는 시간
    /// </summary>
    public float lifeTime = 1.0f;

    /// <summary>
    /// 이동 방향
    /// </summary>
    private Vector3 fireDir;

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

    // 컴포넌트
    Rigidbody rigid;


    // < >  =======================================================================================

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        StartCoroutine(LifeOver());
    }

    private void Update()
    {
        transform.position += Time.deltaTime * bulletSpeed * fireDir;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("EnemyShield") || collision.gameObject.CompareTag("Wall"))
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }


    // <Fuc>    ===================================================================================

    /// <summary>
    /// 총알의 활성화 시간 코루틴
    /// </summary>
    private IEnumerator LifeOver()
    {
        yield return new WaitForSeconds(lifeTime);

        gameObject.SetActive(false);
    }
}
