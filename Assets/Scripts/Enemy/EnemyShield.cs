using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    /// <summary>
    /// 맞으면 바뀔 머테리얼들
    /// </summary>
    public Material[] materials;

    // 컴포넌트
    Renderer[] renderers;

    // < >    =====================================================================================

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어 총알하고 충돌하면 플레이어 총알 비활성화
        if (other.CompareTag("PlayerBullet"))
        {
            other.gameObject.SetActive(false);

            StopAllCoroutines();
            StartCoroutine(OnHitEffect());
        }
    }

    // <Fuc>    ===================================================================================

    /// <summary>
    /// 맞으면 깜박이는 이펙트용 코루틴
    /// </summary>
    private IEnumerator OnHitEffect()
    {
        foreach(Renderer render in renderers)
        {
            render.material = materials[1];
        }

        yield return new WaitForSeconds(0.1f);

        foreach(Renderer render in renderers)
        {
            render.material = materials[0];
        }
    }
}
