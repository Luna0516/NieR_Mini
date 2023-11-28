using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    /// <summary>
    /// 길을 막는 블록 오브젝트들을 모은 부모 오브젝트
    /// </summary>
    GameObject limitBlocks;

    /// <summary>
    /// EnemyBase를 상속받은 게임오브젝트 배열
    /// </summary>
    protected GameObject[] enemys;

    /// <summary>
    /// 소환된 적의 개수 / EnemyBase를 상속 받은 enemys 배열을 정의할 개수 => 소환될 적의 개수가 정해져 있어서...
    /// </summary>
    int enemyCount;

    /// <summary>
    /// 적이 죽으면 보낼 신호의 파라메터
    /// </summary>
    int enemysNum = 1;

    /// <summary>
    /// 소환된 적의 개수 프로퍼티
    /// </summary>
    protected int EnemyCount
    {
        get => enemyCount;
        set
        {
            enemyCount = value;

            if(enemyCount == 0)
            {
                if (limitBlocks)
                {
                    limitBlocks.SetActive(false);
                }
                onEnemysDie?.Invoke(enemysNum++);
            }
        }
    }

    /// <summary>
    /// 현재 소환된 적이 모두 죽었음을 알리는 델리게이트 (파라메터 : 소환된 번호)
    /// </summary>
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
        checkBox.onCheckBoxIn += () =>
        {
            enemys[0].gameObject.SetActive(true);

            foreach (EnemyBase enemy in enemyBases)
            {
                enemy.onDie += () => EnemyCount--;
            }
        };
    }
}
