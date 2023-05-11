using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveManager : MonoBehaviour
{
    // singleton
        private static WaveManager instance = null;
        public static WaveManager Instance => instance;
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
    // singleton
    
    public int CurrentWave = 0;
    public int CurrentWaveTime = 0;
    public int FullWaveTime = 60;

    public event Action<int> OnWaveUpdated;
    public event Action<int> OnWaveTimeUpdated;

    void Start()
    {
        GameManager.Instance.onGameStateChanged += onGameStateChanged;
    }

    private void onGameStateChanged(GameManager.GameState _gameState){
        if(_gameState == GameManager.GameState.Combat){
            InvokeRepeating("StartWaveTimer", 0, 1);
        }else if(_gameState == GameManager.GameState.Upgrade){
            CurrentWaveTime = 0;
            CancelInvoke("StartWaveTimer");
        }
    }

    private void StartWaveTimer(){
        AddTime(1);
    }

    private void AddTime(int _time){


        if(CurrentWaveTime >= FullWaveTime){
            GameManager.Instance.changeGameState(GameManager.GameState.Upgrade);
            CurrentWave++;
        };
        CurrentWaveTime += _time;
        OnWaveTimeUpdated?.Invoke(CurrentWaveTime);

    }

}
