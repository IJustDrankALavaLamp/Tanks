using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] m_Tanks;

    private float m_gameTime = 0;

    public float GameTime { get { return m_gameTime;  } }

    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };

    private GameState m_GameState;
    public GameState State { get { return m_GameState; } }

    private void Awake()
    {
        m_GameState = GameState.Start;
    }

    private void Start()
    {
        for(int i = 0; 1 < m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(false);
        }
    }


}
