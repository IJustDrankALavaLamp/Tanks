using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public HighScores m_HighScores;


    public Text m_MessageText;

    public Text m_TimerText;

    public GameObject[] m_Tanks;

    private float m_gameTime = 0;

    public float GameTime { get { return m_gameTime; } }

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
        m_TimerText.gameObject.SetActive(false);
        m_MessageText.text = "Get Ready";
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(false);
        }


    }
    void Update()
    {
        switch (m_GameState)
        {
            case GameState.Start:
                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    m_TimerText.gameObject.SetActive(true);
                    m_MessageText.text = "";
                    m_GameState = GameState.Playing;

                    for (int i = 0; i < m_Tanks.Length; i++)
                    {
                        m_Tanks[i].SetActive(true);
                    }
                    //lock and hide cursor
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                break;
            case GameState.Playing:
                bool isGameOver = false;

                m_gameTime += Time.deltaTime;
                int seconds = Mathf.RoundToInt(m_gameTime);
                m_TimerText.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));

                if (OneTankLeft() == true)
                {
                    isGameOver = true;
                }
                else if (IsPlayerDead() == true)
                {
                    isGameOver = true;

                }

                if (isGameOver == true)
                {
                    m_GameState = GameState.GameOver;
                    m_TimerText.gameObject.SetActive(false);

                    if(IsPlayerDead() == true)
                    {
                        m_MessageText.text = "YOU LOSE";
                    }
                    else
                    {
                        m_MessageText.text = "You Won!";

                        //save the score
                        m_HighScores.AddScore(Mathf.RoundToInt(m_gameTime));
                        m_HighScores.SaveScoresToFile();
                    }
                }
                break;

            case GameState.GameOver:
                //shows and unlocks cursor
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                //pauses game completely
                


                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    m_gameTime = 0;
                    m_GameState = GameState.Playing;
                    m_MessageText.text = "";
                    m_TimerText.gameObject.SetActive(true);

                    for (int i = 0; i < m_Tanks.Length; i++)
                    {
                        m_Tanks[i].SetActive(true);
                    }
                }
                break;
        }


        if (Input.GetKeyUp(KeyCode.Q))
        {
            Time.timeScale = 1 - Time.timeScale; //will change if the game is paused
            
            if (Time.timeScale == 0)//if paused will turn on the cursor and disable the aiming/shooting
            {
                m_MessageText.text = "Paused";

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                GameObject.Find("Tank").GetComponent<TankShot>().enabled = false;
                GameObject.Find("Tank").GetComponentInChildren<TankAim>().enabled = false;

            } 

            else
            {
                m_MessageText.text = "";
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameObject.Find("Tank").GetComponent<TankShot>().enabled = true;
                GameObject.Find("Tank").GetComponentInChildren<TankAim>().enabled = true;
            }
        }


    }
    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].activeSelf == true)
            {
                numTanksLeft++;
            }
        }
        return numTanksLeft <= 1;
    }

    private bool IsPlayerDead()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].activeSelf == false)
            {
                if (m_Tanks[i].tag == "Player")
                    return true;
            }
        }


        return false;
    }
}


