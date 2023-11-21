using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFinal : EnemyBase
{
    Transform shieldTr;

    public System.Action onAllEnemyDie;

    protected override void Init_Awake()
    {
        base.Init_Awake();

        shieldTr = transform.GetChild(1);

        onAllEnemyDie += DestroyShield;

        render = transform.GetChild(0).GetComponent<Renderer>();
    }

    protected override void Die()
    {
        base.Die();

        GameManager.Inst.isClear = true;

        GameManager.Inst.State = GameManager.GameState.End;

        gameObject.SetActive(false);
    }

    private void DestroyShield()
    {
        shieldTr.gameObject.SetActive(false);
        onAllEnemyDie = null;
    }
}
