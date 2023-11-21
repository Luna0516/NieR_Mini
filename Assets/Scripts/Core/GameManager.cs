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
                        Cursor.visible = true;
                        break;
                    case GameState.Play:
                        Cursor.visible = false;
                        break;
                    case GameState.End:
                        StartCoroutine(GameEndCoroutine());
                        break;
                    case GameState.None:
                    default:
                        break;
                }
            }
        }
    }

    public bool isClear = false;

    public System.Action<bool, float> onGameEnd;

    private IEnumerator GameEndCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        Cursor.visible = true;

        onGameEnd?.Invoke(isClear, Player.PlayTime);
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();

        isClear = false;
        onGameEnd = null;
    }
}
