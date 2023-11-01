using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    /// <summary>
    /// 적의 체력
    /// </summary>
    public int hp = 5;

    /// <summary>
    /// HP 설정용 프로퍼티
    /// </summary>
    public int HP
    {
        get => hp;
        set
        {
            if (hp != value)
            {
                if (hp > value)
                {
                    StartCoroutine(OnHitEffect());
                }

                hp = value;

                if (hp <= 0)
                {
                    Die();
                }
            }
        }
    }

    /// <summary>
    /// 회전 속도
    /// </summary>
    protected float rotSpeed = 0.0f;

    /// <summary>
    /// 총알 발사 시간 확인용 경과 시간
    /// </summary>
    protected float elapsedTIme = 0.0f;

    /// <summary>
    /// 총알 발사 딜레이 시간
    /// </summary>
    protected float fireDelay = 0.0f;

    /// <summary>
    /// 발사할 총알 종류
    /// </summary>
    public EnemyBulletType bulletType = EnemyBulletType.None;

    /// <summary>
    /// Material 일반용, Hit용
    /// </summary>
    public Material[] materials;

    /// <summary>
    /// 풀매니저
    /// </summary>
    protected PoolManager poolManager;

    // 컴포넌트
    Renderer render;

    // < > ========================================================================================

    private void Awake()
    {
        render = GetComponent<Renderer>();

        Init_Awake();
    }

    protected virtual void Start()
    {
        poolManager = PoolManager.Inst;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            OnHit(1);
        }
    }

    // <기타 함수> ====================================================================================

    /// <summary>
    /// Awake에서 초기화 할 함수
    /// </summary>
    protected virtual void Init_Awake()
    {

    }

    /// <summary>
    /// 총알 발사 함수
    /// </summary>
    protected virtual void Fire()
    {

    }

    /// <summary>
    /// 맞으면 데미지 처리하는 함수
    /// </summary>
    /// <param name="damage"></param>
    private void OnHit(int damage)
    {
        HP -= damage;
    }

    /// <summary>
    /// 죽으면 실행할 함수
    /// </summary>
    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 맞으면 깜박이는 이펙트용 코루틴
    /// </summary>
    private IEnumerator OnHitEffect()
    {
        render.material = materials[1];

        yield return new WaitForSeconds(0.1f);

        render.material = materials[0];
    }
}