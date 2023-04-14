using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    //Singleton
        private static GameManager instance = null;
        public static GameManager Instance => instance;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(this.gameObject);
        }
    //Singleton

    public enum GameState{
        Upgrade,
        Combat,
    }
    public GameState gameState;
    public event Action<GameState> onGameStateChanged;
    public int wave = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Upgrade;
    }

    public void changeGameState(GameState _gameState){
        gameState = _gameState;
        onGameStateChanged?.Invoke(gameState);
    }

    
}
