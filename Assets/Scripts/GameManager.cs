using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState 
{ 
    MAIN_MENU,
    NUMBERS
}

public delegate void OnStateChangeHandler();

public class GameManager : ScriptableObject
{
    private GameManager() { }
    private static GameManager instance = null;
    public event OnStateChangeHandler OnStateChange;
    public GameState gameState { get; private set; }

    public static GameManager Instance
    {
        get
        {
            if (GameManager.instance == null)
            {
                GameManager.instance = CreateInstance("GameManager") as GameManager;
                DontDestroyOnLoad(GameManager.instance);
            }

            return GameManager.instance;
        }
    }

    public void SetGameState(GameState state)
    {
        this.gameState = state;
        OnStateChange();
    }

    public void OnApplicationQuit()
    {
        GameManager.instance = null;
    }
}
