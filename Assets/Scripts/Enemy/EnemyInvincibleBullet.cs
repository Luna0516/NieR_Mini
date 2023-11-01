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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}

