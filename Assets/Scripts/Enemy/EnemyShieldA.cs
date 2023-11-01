using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldA : MonoBehaviour
{
    private void Awake()
    {
        EnemyA enemyA = GetComponentInChildren<EnemyA>();

        enemyA.onDie += SetDeActive;
    }

    private void SetDeActive()
    {
        gameObject.SetActive(false);
    }
}
