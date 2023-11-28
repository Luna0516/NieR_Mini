using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    /// <summary>
    /// 체크박스 안에 들어왔다고 알리는 델리게이트 신호
    /// </summary>
    public System.Action onCheckBoxIn;

    private void OnTriggerStay(Collider other)
    {
        // 플레이어가 트리거 안으로 들어오면
        if (other.CompareTag("Player"))
        {
            // 체크박스 안에 들어왔다고 델리게이트 신호 보내기
            onCheckBoxIn?.Invoke();
            // 델리게이트 연결된거 해제
            onCheckBoxIn = null;
            // 부모 오브젝트 해제
            transform.parent = null;
            // 게임 오브젝트 삭제 => 한번만 실행시키기 위해서
            Destroy(gameObject);
        }
    }
}
