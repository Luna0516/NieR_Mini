using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyFinal : SpawnEnemy
{
    EnemyFinal enemyFinal;

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
        checkBox.onCheck += () => NextSpawn(0);
    }

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
