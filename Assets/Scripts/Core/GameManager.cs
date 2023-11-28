using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 게임 상태 enum
    /// </summary>
    public enum GameState
    {
        None,
        Ready,
        Play,
        End
    }

    /// <summary>
    /// 게임이 클리어 되었는지 확인하는 변수
    /// </summary>
    public bool isClear = false;

    /// <summary>
    /// 플레이어
    /// </summary>
    private Player player;

    /// <summary>
    /// 플레이어 프로퍼티 (get 전용)
    /// </summary>
    public Player Player
    {
        get
        {
            // 플레이어가 null이면
            if(!player)
            {
                player = FindObjectOfType<Player>();
            }

            return player;
        }
    }

    /// <summary>
    /// 게임 상태
    /// </summary>
    private GameState state = GameState.None;

    /// <summary>
    /// 게임 상태 프로퍼티
    /// </summary>
    public GameState State
    {
        get => state;
        set
        {
            if(state != value)
            {
                state = value;

                switch (state)
                {
                    case GameState.Ready:
                        // 마우스 커서 보이게 하기
                        Cursor.visible = true;
                        break;
                    case GameState.Play:
                        // 마우스 커서 안보이게 하기
                        Cursor.visible = false;
                        break;
                    case GameState.End:
                        // 게임 종료 코루틴 실행
                        StartCoroutine(GameEndCoroutine());
                        break;
                    case GameState.None:
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 게임이 종료되면 보낼 델리게이트 (파라메터 : 클리어 했는지 확인용 bool, 클리어 하는데 걸리는 시간 float)
    /// </summary>
    public System.Action<bool, float> onGameEnd;

    /// <summary>
    /// 게임 종료 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameEndCoroutine()
    {
        // 0.2초 뒤에
        yield return new WaitForSeconds(0.2f);

        // 커서 보이게 하기
        Cursor.visible = true;

        // 델리게이트 신호 보내기(클리어 여부, 플레이타임)
        onGameEnd?.Invoke(isClear, Player.PlayTime);
    }

    /// <summary>
    /// 씬이 로딩 될때마다 실행할 함수
    /// </summary>
    protected override void OnInitialize()
    {
        base.OnInitialize();

        // 클리어 변수 초기화
        isClear = false;

        // 델리게이트 초기화
        onGameEnd = null;
    }
}
