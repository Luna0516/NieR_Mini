using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletB : MonoBehaviour
{
    float moveSpeed = 5.0f;

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
