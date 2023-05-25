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
    private int EndWaveTime = 0;
    public int FullWaveTime = 60;

    public float timeBetweenSpawn{get; private set;} = 5;
    public float maxWeight{get; private set;} = 5;
    public int currentWeight{get; private set;} = 0;
    public bool stopSpowning{get; private set;} = false;

    public void addEnemy(int weight){
        currentWeight += weight;
    }

    public bool isMaxEnemies(){
        if(currentWeight >= maxWeight){
            return true;
        }
        return false;
    }

    public event Action<int> OnWaveUpdated;
    public event Action<int> OnWaveTimeUpdated;

    void Start()
    {
        GameManager.Instance.onGameStateChanged += onGameStateChanged;
        UnitEvent.OnUnitDie += OnUnitDie;
    }

    private void OnUnitDie(UnitController unitController){
        if(stopSpowning == true && currentWeight <= 0){
            endWave();
            return;
        }
        if(unitController.isAlly()){
           return;
        }
        currentWeight -= unitController.Weight;
    }

    private void onGameStateChanged(GameManager.GameState _gameState){
        if(_gameState == GameManager.GameState.Combat){
            CurrentWave++;
            InvokeRepeating("StartWaveTimer", 0, 1);
            OnWaveUpdated?.Invoke(CurrentWave);
        }else if(_gameState != GameManager.GameState.Combat){
            CancelInvoke("StartWaveTimer");
        }
    }

    private void StartWaveTimer(){
        AddTime(1);
    }

    private void AddTime(int _time){
        if(CurrentWaveTime >= FullWaveTime){
            stopSpowning = true;
            if(EndWaveTime == 30){
                endWave();
            }else{
                EndWaveTime++;
            }
        }else{
            CurrentWaveTime += _time;
            OnWaveTimeUpdated?.Invoke(CurrentWaveTime);
        };
    }

    private void endWave(){
        GameManager.Instance.changeGameState(GameManager.GameState.Upgrade);

        
        stopSpowning = false;
        EndWaveTime = 0;
        CurrentWaveTime = 0;
        currentWeight = 0;
        maxWeight += 2.5f;
        FullWaveTime += 15;
        if(timeBetweenSpawn > 0.5f){
            timeBetweenSpawn -= 0.5f;
        }
        
    }

}
