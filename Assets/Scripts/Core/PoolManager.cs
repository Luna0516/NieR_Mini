using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;

    public static PoolManager Inst => instance;

    /// <summary>
    /// Pool 구조체
    /// </summary>
    [System.Serializable]
    public struct Pool
    {
        /// <summary>
        /// 풀의 크기
        /// </summary>
        public int size;

        /// <summary>
        /// 풀의 부모 트랜스폼
        /// </summary>
        public Transform poolParent;

        /// <summary>
        /// 풀의 오브젝트 프리펩
        /// </summary>
        public GameObject prefab;
    }

    /// <summary>
    /// 플레이어 총알 풀
    /// </summary>
    public Pool playerBulletPool;

    /// <summary>
    /// 적 총알 풀 (기본)
    /// </summary>
    public Pool enemyBulletPool;

    /// <summary>
    /// 적 총알 풀 (파괴 안됨)
    /// </summary>
    public Pool enemyInvincibleBulletPool;

    /// <summary>
    /// PlayerBulletPool List
    /// </summary>
    private List<PlayerBullet> playerBulletList = new List<PlayerBullet>();

    /// <summary>
    /// EnemyBulletPool List
    /// </summary>
    private List<EnemyBullet> enemyBulletList = new List<EnemyBullet>();

    /// <summary>
    /// EnemyInvincibleBulletPool List
    /// </summary>
    private List<EnemyInvincibleBullet> enemyInvincibleBulletList = new List<EnemyInvincibleBullet>();

    // <> =========================================================================================

    private void Awake()
    {
        instance = this;

        SetPlayerBulletPool();
        SetEnemyBulletPool();
        SetEnemyInvincibleBulletPool();
    }

    // <Pool 생성 & 확장 함수> ============================================================================

    /// <summary>
    /// PlayerBulletPool 생성
    /// </summary>
    private void SetPlayerBulletPool()
    {
        for (int i = 0; i < playerBulletPool.size; i++)
        {
            GameObject playerBulletObj = Instantiate(playerBulletPool.prefab, playerBulletPool.poolParent);
            playerBulletObj.name = $"PlayerBullet_{i}";

            PlayerBullet bullet = playerBulletObj.GetComponent<PlayerBullet>();

            playerBulletList.Add(bullet);

            playerBulletObj.SetActive(false);
        }
    }

    /// <summary>
    /// EnemyBulletPool 생성
    /// </summary>
    private void SetEnemyBulletPool()
    {
        for (int i = 0; i < enemyBulletPool.size; i++)
        {
            GameObject enemyBulletObj = Instantiate(enemyBulletPool.prefab, enemyBulletPool.poolParent);
            enemyBulletObj.name = $"EnemyBullet_{i}";

            EnemyBullet bullet = enemyBulletObj.GetComponent<EnemyBullet>();

            enemyBulletList.Add(bullet);

            enemyBulletObj.SetActive(false);
        }
    }

    /// <summary>
    /// EnemyInvincibleBulletPool 생성
    /// </summary>
    private void SetEnemyInvincibleBulletPool()
    {
        for (int i = 0; i < enemyInvincibleBulletPool.size; i++)
        {
            GameObject enemyBulletObj = Instantiate(enemyInvincibleBulletPool.prefab, enemyInvincibleBulletPool.poolParent);
            enemyBulletObj.name = $"EnemyInvincibleBullet_{i}";

            EnemyInvincibleBullet bullet = enemyBulletObj.GetComponent<EnemyInvincibleBullet>();

            enemyInvincibleBulletList.Add(bullet);

            enemyBulletObj.SetActive(false);
        }
    }

    /// <summary>
    /// PlayerBulletPool 확장 함수 (1개씩 증가)
    /// </summary>
    /// <returns>확장하고 난 풀의 PlayerBullet</returns>
    private PlayerBullet ExtendPlayerBulletPoolSize()
    {
        playerBulletPool.size++;

        GameObject playerBulletObj = Instantiate(playerBulletPool.prefab, playerBulletPool.poolParent);
        playerBulletObj.name = $"PlayerBullet_{playerBulletPool.size}";

        PlayerBullet bullet = playerBulletObj.GetComponent<PlayerBullet>();

        playerBulletList.Add(bullet);

        playerBulletObj.SetActive(false);

        return bullet;
    }

    /// <summary>
    /// EnemyBulletPool 확장 함수 (1개씩 증가)
    /// </summary>
    /// <returns>확장하고 난 풀의 EnemyBullet</returns>
    private EnemyBullet ExtendEnemyBulletPoolSize()
    {
        enemyBulletPool.size++;

        GameObject enemyBulletObj = Instantiate(enemyBulletPool.prefab, enemyBulletPool.poolParent);
        enemyBulletObj.name = $"EnemyBullet_{enemyBulletPool.size}";

        EnemyBullet bullet = enemyBulletObj.GetComponent<EnemyBullet>();
        enemyBulletList.Add(bullet);

        enemyBulletObj.SetActive(false);

        return bullet;
    }

    /// <summary>
    /// EnemyInvincibleBulletPool 확장 함수 (1개씩 증가)
    /// </summary>
    /// <returns>확장하고 난 풀의 EnemyInvincibleBullet</returns>
    private EnemyInvincibleBullet ExtendEnemyInvincibleBulletPoolSize()
    {
        enemyInvincibleBulletPool.size++;

        GameObject enemyBulletObj = Instantiate(enemyInvincibleBulletPool.prefab, enemyInvincibleBulletPool.poolParent);
        enemyBulletObj.name = $"EnemyInvincibleBulletPool_{enemyInvincibleBulletPool.size}";

        EnemyInvincibleBullet bullet = enemyBulletObj.GetComponent<EnemyInvincibleBullet>();
        enemyInvincibleBulletList.Add(bullet);

        enemyBulletObj.SetActive(false);

        return bullet;
    }

    // <Pool에 있는 오브젝트 가져오는 함수> ======================================================================

    /// <summary>
    /// PlayerBulletPool에서 비활성화된 PlayerBullet 가져오기
    /// </summary>
    /// <returns>현재 List에서 비활성화가 된 PlayerBullet (null 리턴시 풀에 남아있는 비활성화 오브젝트 없음)</returns>
    private PlayerBullet GetPlayerBullet()
    {
        PlayerBullet playerBullet = null;

        foreach(PlayerBullet bullet in playerBulletList)
        {
            if (!bullet.gameObject.activeSelf)
            {
                playerBullet = bullet;
                return playerBullet;
            }
        }

        if (!playerBullet)
        {
            playerBullet = ExtendPlayerBulletPoolSize();
        }

        return playerBullet;
    }

    /// <summary>
    /// EnemyBulletPool에서 비활성화된 EnemyBullet 가져오기
    /// </summary>
    /// <returns>현재 List에서 비활성화가 된 EnemyBullet (null 리턴시 풀에 남아있는 비활성화 오브젝트 없음)</returns>
    private EnemyBullet GetEnemyBullet()
    {
        EnemyBullet enemyBullet = null;

        foreach (EnemyBullet bullet in enemyBulletList)
        {
            if (!bullet.gameObject.activeSelf)
            {
                enemyBullet = bullet;
                return enemyBullet;
            }
        }

        if (!enemyBullet)
        {
            enemyBullet = ExtendEnemyBulletPoolSize();
        }

        return enemyBullet;
    }

    /// <summary>
    /// EnemyInvincibleBulletPool에서 비활성화된 EnemyInvincibleBullet 가져오기
    /// </summary>
    /// <returns>현재 List에서 비활성화가 된 EnemyInvincibleBullet (null 리턴시 풀에 남아있는 비활성화 오브젝트 없음)</returns>
    private EnemyInvincibleBullet GetEnemyInvincibleBullet()
    {
        EnemyInvincibleBullet enemyBullet = null;

        foreach (EnemyInvincibleBullet bullet in enemyInvincibleBulletList)
        {
            if (!bullet.gameObject.activeSelf)
            {
                enemyBullet = bullet;
                return enemyBullet;
            }
        }

        if (!enemyBullet)
        {
            enemyBullet = ExtendEnemyInvincibleBulletPoolSize();
        }

        return enemyBullet;
    }

    // <외부에서 Pool에 있는 오브젝트 가져오는 함수> =================================================================

    /// <summary>
    /// 플레이어 총알 생성해주는 함수
    /// </summary>
    /// <param name="_fireDir">총알이 나아가야할 방향</param>
    /// <param name="spawnPos">총알을 생성할 위치</param>
    public void GetPlayerBullet(Vector3 _fireDir, Vector3 spawnPos)
    {
        PlayerBullet bullet = GetPlayerBullet();

        bullet.FireDir = _fireDir;

        bullet.gameObject.transform.position = spawnPos;

        bullet.gameObject.SetActive(true);
    }

    /// <summary>
    /// 적 총알 생성해주는 함수 (Normal)
    /// </summary>
    /// <param name="_type">적 총알의 종류</param>
    /// <param name="moveDir">적 총알의 이동 방향</param>
    /// <param name="spawnPos">적 총알의 생성 위치</param>
    public void GetEnemyBullet(Vector3 moveDir, Vector3 spawnPos)
    {
        EnemyBullet bullet = GetEnemyBullet();

        bullet.transform.forward = moveDir;

        bullet.gameObject.transform.position = spawnPos;

        bullet.gameObject.SetActive(true);
    }

    /// <summary>
    /// 적 총알 생성해주는 함수 (Invincible)
    /// </summary>
    /// <param name="_type">적 총알의 종류</param>
    /// <param name="moveDir">적 총알의 이동 방향</param>
    /// <param name="spawnPos">적 총알의 생성 위치</param>
    public void GetEnemyInvincibleBullet(Vector3 moveDir, Vector3 spawnPos)
    {
        EnemyInvincibleBullet bullet = GetEnemyInvincibleBullet();

        bullet.transform.forward = moveDir;

        bullet.gameObject.transform.position = spawnPos;

        bullet.gameObject.SetActive(true);
    }
}
