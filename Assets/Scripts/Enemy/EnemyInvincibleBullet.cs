using UnityEngine;
using System.Collections;

public class EnemyInvincibleBullet : MonoBehaviour
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
        if(collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}

