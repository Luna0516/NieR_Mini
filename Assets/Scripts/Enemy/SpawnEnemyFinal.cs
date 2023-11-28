using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyFinal : SpawnEnemy
{
    /// <summary>
    /// 최종 보스
    /// </summary>
    EnemyFinal enemyFinal;

    /// <summary>
    /// 총 소환 횟수
    /// </summary>
    int finalSpawnCount = 4;

    protected override void Awake()
    {
        enemys = new GameObject[finalSpawnCount];

        for (int i = 0; i < finalSpawnCount; i++)
        {
            enemys[i] = transform.GetChild(i+2).gameObject;
            enemys[i].gameObject.SetActive(false);
        }

        enemyFinal = GetComponentInChildren<EnemyFinal>();

        onEnemysDie += NextSpawn;

        CheckBox checkBox = GetComponentInChildren<CheckBox>();
        checkBox.onCheckBoxIn += () => NextSpawn(0);
    }

    /// <summary>
    /// 몬스터 웨이브가 다 죽으면 다음 몬스터 웨이브 소환하는 함수
    /// </summary>
    /// <param name="num">몇번째 웨이브인지 확인</param>
    private void NextSpawn(int num)
    {
        enemys[num].SetActive(true);

        EnemyBase[] enemyBase = enemys[num].GetComponentsInChildren<EnemyBase>();

        EnemyCount = enemyBase.Length;

        foreach(EnemyBase enemy in enemyBase)
        {
            enemy.onDie += () => EnemyCount--;
        }

        if(num == finalSpawnCount - 1)
        {
            onEnemysDie = null;
            onEnemysDie += (_) => enemyFinal.onAllEnemyDie?.Invoke();
        }
    }
}
