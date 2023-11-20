using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Inst => instance;

    private Player player;

    public Player Player
    {
        get
        {
            if(player == null)
            {
                player = FindObjectOfType<Player>();
            }

            return player;
        }
    }

    public Transform playerStartPos;

    public System.Action onClear;

    private void Awake()
    {
        instance = this;
    }
}
