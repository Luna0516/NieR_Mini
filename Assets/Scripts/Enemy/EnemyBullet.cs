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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerBullet"))
        {
            gameObject.SetActive(false);
        }
    }
}
