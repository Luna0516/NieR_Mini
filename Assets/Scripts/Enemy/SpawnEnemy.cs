using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    GameObject limitBlocks;
    protected GameObject[] enemys;

    int enemyCount;

    protected int enemysNum = 1;

    protected int EnemyCount
    {
        get => enemyCount;
        set
        {
            enemyCount = value;

            if(enemyCount == 0)
            {
                if (limitBlocks != null)
                {
                    limitBlocks.SetActive(false);
                }
                onEnemysDie?.Invoke(enemysNum++);
            }
        }
    }

    protected System.Action<int> onEnemysDie;

    protected virtual void Awake()
    {
        enemys = new GameObject[enemysNum];

        EnemyBase[] enemyBases = GetComponentsInChildren<EnemyBase>();

        enemyCount = enemyBases.Length;

        enemys[0] = transform.GetChild(2).gameObject;

        enemys[0].gameObject.SetActive(false);

        limitBlocks = transform.GetChild(0).gameObject;

        CheckBox checkBox = GetComponentInChildren<CheckBox>();
        checkBox.onCheck += () =>
        {
            enemys[0].gameObject.SetActive(true);

            foreach (EnemyBase enemy in enemyBases)
            {
                enemy.onDie += () => EnemyCount--;
            }
        };
    }
}
