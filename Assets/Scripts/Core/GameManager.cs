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

    private void Awake()
    {
        instance = this;
    }
}
