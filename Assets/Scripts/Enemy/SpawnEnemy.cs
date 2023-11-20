using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    EnemyBase[] enemyBases;

    GameObject limitBlocks;
    GameObject enemys;

    int enemyCount;

    private int EnemyCount
    {
        get => enemyCount;
        set
        {
            enemyCount = value;

            if(enemyCount == 0)
            {
                limitBlocks.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        enemyBases = GetComponentsInChildren<EnemyBase>();

        enemyCount = enemyBases.Length;

        enemys = transform.GetChild(2).gameObject;

        enemys.gameObject.SetActive(false);

        limitBlocks = transform.GetChild(0).gameObject;

        CheckBox checkBox = GetComponentInChildren<CheckBox>();
        checkBox.onCheck += () =>
        {
            enemys.gameObject.SetActive(true);

            foreach (EnemyBase enemy in enemyBases)
            {
                enemy.onDie += () => EnemyCount--;
            }
        };
    }
}
