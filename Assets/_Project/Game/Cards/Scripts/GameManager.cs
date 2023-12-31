using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game")]
    public GameObject gameOverUIPrefab;
    public StatsData difficulty;

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

    public GameObject playerHealthZone;
    public int life{get; private set;} = 3;
    public event Action<int> onLifeChanged;

    public enum GameState{
        Upgrade,
        Combat,
        GameOver,
    }
    public GameState gameState;
    public event Action<GameState> onGameStateChanged;


    public void removeOneLife(){
        life -= 1;
        onLifeChanged?.Invoke(life);
        if(life <= 0){
            gameOver();
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

    public void gameOver() {
        GameManager.Instance.changeGameState(GameManager.GameState.GameOver);
        Instantiate(gameOverUIPrefab);
        StartCoroutine(waitOnGameOver());
    }

    IEnumerator waitOnGameOver() {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("HubScene", LoadSceneMode.Single);
    }
}
