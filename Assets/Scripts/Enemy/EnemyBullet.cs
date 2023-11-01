using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    /// <summary>
    /// 총알의 이동 속도
    /// </summary>
    public float moveSpeed = 5.0f;

    private void Update()
    {
        transform.position += Time.deltaTime * moveSpeed * transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Player") || other.CompareTag("PlayerBullet"))
        {
            gameObject.SetActive(false);
        }
    }
}
