using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    public float rotSpeed = 45.0f;

    public float rotDir = -1;

    private void Update()
    {
        transform.Rotate(transform.up, Time.deltaTime * rotSpeed * rotDir);
    }
}
