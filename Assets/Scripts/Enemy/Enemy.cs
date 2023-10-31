using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyBulletType
{
    BulletA,
    BulletB
}

public class Enemy : MonoBehaviour
{
    public EnemyBulletType bulletType;

    private int hp = 5;

    public int HP
    {
        get => hp;
        set
        {
            if(hp != value)
            {
                hp = value;

                StartCoroutine(OnHitEffect());

                if(hp <= 0)
                {
                    Die();
                }
            }
        }
    }

    float rotSpeed = 30.0f;

    float elapsedTIme = 0.0f;

    float fireDelay = 0.75f;

    public Material[] materials;

    public GameObject enemyBullet;

    PoolManager poolManager;

    Renderer render;

    private void Awake()
    {
        render = GetComponent<Renderer>();
    }

    private void Start()
    {
        poolManager = PoolManager.Inst;
    }

    private void Update()
    {
        elapsedTIme += Time.deltaTime;

        transform.Rotate(transform.up, Time.deltaTime * rotSpeed);

        if(elapsedTIme > fireDelay)
        {
            Fire();
            elapsedTIme = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            OnHit();
        }
    }

    private void Fire()
    {
        if (bulletType == EnemyBulletType.BulletA)
        {
            poolManager.GetEnemyBulletA(transform.forward, transform.position);
            poolManager.GetEnemyBulletA(-transform.forward, transform.position);
            poolManager.GetEnemyBulletA(-transform.right, transform.position);
            poolManager.GetEnemyBulletA(transform.right, transform.position);
        }
        else
        {
            poolManager.GetEnemyBulletB(transform.forward, transform.position);
            poolManager.GetEnemyBulletB(-transform.forward, transform.position);
            poolManager.GetEnemyBulletB(-transform.right, transform.position);
            poolManager.GetEnemyBulletB(transform.right, transform.position);
        }
    }

    private void OnHit()
    {
        HP--;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    IEnumerator OnHitEffect()
    {
        render.material = materials[1];

        yield return new WaitForSeconds(0.1f);

        render.material = materials[0];
    }
}
