using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestEnemyFinal : TestBase
{
    public EnemyFinal enemyFinal;

    protected override void Test1(InputAction.CallbackContext context)
    {
        enemyFinal.onAllEnemyDie?.Invoke();
    }
}
