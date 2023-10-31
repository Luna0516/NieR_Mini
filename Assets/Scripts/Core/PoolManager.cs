using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;

    public static PoolManager Inst => instance;

    public GameObject playerBulletPrefab;

    public GameObject EnemyBulletPrefab1;

    public GameObject EnemyBulletPrefab2;

    private List<PlayerBullet> playerBulletList;

    private List<GameObject> enemyBulletAList;

    private List<GameObject> enemyBulletBList;

    int playerBulletListSize = 8;

    int enemyBulletAListSize = 256;

    int enemyBulletBListSize = 128;

    Transform playerBulletTr;

    Transform enemyBulletATr;

    Transform enemyBulletBTr;

    private void Awake()
    {
        instance = this;

        playerBulletTr = transform.GetChild(0);
        enemyBulletATr = transform.GetChild(1);
        enemyBulletBTr = transform.GetChild(2);

        playerBulletList = new List<PlayerBullet>();
        enemyBulletAList = new List<GameObject>();
        enemyBulletBList = new List<GameObject>();

        SetPlayerBulletPool();
        SetEnemyBulletPool();
    }

    private void SetPlayerBulletPool()
    {
        for (int i = 0; i < playerBulletListSize; i++)
        {
            GameObject playerBulletObj = Instantiate(playerBulletPrefab, playerBulletTr);
            playerBulletObj.name = $"PlayerBullet_{i}";

            PlayerBullet bullet = playerBulletObj.GetComponent<PlayerBullet>();

            playerBulletList.Add(bullet);

            playerBulletObj.SetActive(false);
        }
    }

    private PlayerBullet ExtendPlayerBulletPoolSize()
    {
        playerBulletListSize++;

        GameObject playerBulletObj = Instantiate(playerBulletPrefab, playerBulletTr);
        playerBulletObj.name = $"PlayerBullet_{playerBulletListSize}";

        PlayerBullet bullet = playerBulletObj.GetComponent<PlayerBullet>();

        playerBulletList.Add(bullet);

        playerBulletObj.SetActive(false);

        return bullet;
    }

    private PlayerBullet GetPlayerBullet()
    {
        PlayerBullet bulletObj = null;

        foreach(PlayerBullet bullet in playerBulletList)
        {
            if (!bullet.gameObject.activeSelf)
            {
                bulletObj = bullet;
                return bulletObj;
            }
        }

        if (!bulletObj)
        {
            bulletObj = ExtendPlayerBulletPoolSize();
        }

        return bulletObj;
    }

    public void GetPlayerBullet(Vector3 _fireDir, Vector3 spawnPos)
    {
        PlayerBullet bulletObj = GetPlayerBullet();

        bulletObj.fireDir = _fireDir;

        bulletObj.gameObject.transform.position = spawnPos;

        bulletObj.gameObject.SetActive(true);
    }

    private void SetEnemyBulletPool()
    {
        for(int i = 0; i < enemyBulletAListSize; i++)
        {
            GameObject bullet = Instantiate(EnemyBulletPrefab1, enemyBulletATr);

            bullet.name = $"EnemyBulletA_{i}";

            enemyBulletAList.Add(bullet);

            bullet.SetActive(false);
        }

        for (int i = 0; i < enemyBulletBListSize; i++)
        {
            GameObject bullet = Instantiate(EnemyBulletPrefab2, enemyBulletBTr);

            bullet.name = $"EnemyBulletB_{i}";

            enemyBulletBList.Add(bullet);

            bullet.SetActive(false);
        }
    }

    private GameObject ExtendEnemyBulletAPoolSize()
    {
        enemyBulletAListSize++;

        GameObject enemyBulletA = Instantiate(EnemyBulletPrefab1, enemyBulletATr);

        enemyBulletA.name = $"EnemyBulletA_{enemyBulletAListSize}";

        enemyBulletAList.Add(enemyBulletA);

        enemyBulletA.SetActive(false);

        return enemyBulletA;
    }

    private GameObject ExtendEnemyBulletBPoolSize()
    {
        enemyBulletBListSize++;

        GameObject enemyBulletB = Instantiate(EnemyBulletPrefab2, enemyBulletBTr);

        enemyBulletB.name = $"EnemyBulletA_{enemyBulletBListSize}";

        enemyBulletBList.Add(enemyBulletB);

        enemyBulletB.SetActive(false);

        return enemyBulletB;
    }

    private GameObject GetEnemyBulletA()
    {
        GameObject bulletObj = null;

        foreach (GameObject bullet in enemyBulletAList)
        {
            if (!bullet.gameObject.activeSelf)
            {
                bulletObj = bullet;
                return bulletObj;
            }
        }

        if (!bulletObj)
        {
            bulletObj = ExtendEnemyBulletAPoolSize();
        }

        return bulletObj;
    }

    private GameObject GetEnemyBulletB()
    {
        GameObject bulletObj = null;

        foreach (GameObject bullet in enemyBulletBList)
        {
            if (!bullet.gameObject.activeSelf)
            {
                bulletObj = bullet;
                return bulletObj;
            }
        }

        if (!bulletObj)
        {
            bulletObj = ExtendEnemyBulletBPoolSize();
        }

        return bulletObj;
    }

    public void GetEnemyBulletA(Vector3 moveDir, Vector3 spawnPos)
    {
        GameObject bulletObj = GetEnemyBulletA();

        bulletObj.transform.forward = moveDir;

        bulletObj.gameObject.transform.position = spawnPos;

        bulletObj.gameObject.SetActive(true);
    }

    public void GetEnemyBulletB(Vector3 moveDir, Vector3 spawnPos)
    {
        GameObject bulletObj = GetEnemyBulletB();

        bulletObj.transform.forward = moveDir;

        bulletObj.gameObject.transform.position = spawnPos;

        bulletObj.gameObject.SetActive(true);
    }
}
