using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    /// <summary>
    /// 총알의 이동 속도
    /// </summary>
    float bulletSpeed = 15.0f;

    /// <summary>
    /// 총알의 생성되어 있는 시간
    /// </summary>
    float lifeTime = 1.0f;

    public Vector3 fireDir = Vector3.zero;

    private void OnEnable()
    {
        //StartCoroutine(LifeOver());
    }

    private void Update()
    {
        transform.forward = fireDir;
        transform.position += Time.deltaTime * bulletSpeed * fireDir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 총알의 활성화 시간 코루틴
    /// </summary>
    private IEnumerator LifeOver()
    {
        yield return new WaitForSeconds(lifeTime);

        gameObject.SetActive(false);
    }
}
