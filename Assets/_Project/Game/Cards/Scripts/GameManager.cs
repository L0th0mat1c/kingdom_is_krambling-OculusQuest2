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
        }
    //Singleton

    public int life{get; private set;} = 3;
    public event Action<int> onLifeChanged;

    public enum GameState{
        Upgrade,
        Combat,
        GameOver,
    }
    public GameState gameState;
    public event Action<GameState> onGameStateChanged;
    public int wave = 0;


    public void removeOneLife(){
        life -= 1;
        onLifeChanged?.Invoke(life);
        if(life <= 0){
            gameState = GameState.GameOver;
        };
    }

    void Start()
    {
        gameState = GameState.Upgrade;
    }

    public void changeGameState(GameState _gameState){
        gameState = _gameState;
        onGameStateChanged?.Invoke(gameState);
    }

    
}
