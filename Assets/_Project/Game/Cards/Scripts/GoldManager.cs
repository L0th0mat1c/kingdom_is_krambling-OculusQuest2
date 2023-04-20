using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoldManager : MonoBehaviour
{
    
    //Singleton
        private static GoldManager instance = null;
        public static GoldManager Instance => instance;
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

    public int goldBag = 5;
    public int coins = 0;

    public event Action<int> OnGoldBagUpdated;
    public event Action<int> OnCoinUpdated;

    void Start()
    {
        GameManager.Instance.onGameStateChanged += onGameStateChanged;
    }

    private void onGameStateChanged(GameManager.GameState _gameState){
        if(_gameState == GameManager.GameState.Combat){
            InvokeRepeating("autoAddMoney", 0, 1);
        }else if(_gameState == GameManager.GameState.Upgrade){
            goldBag = 0;
            coins = 0;
            CancelInvoke("autoAddMoney");
        }
    }


    private void autoAddMoney(){
        AddMoney(10);
    }

    private void AddMoney(int _coins){
        if(goldBag >= 10)return;
        coins += _coins;
        OnCoinUpdated?.Invoke(coins);
        if(coins >= 100){
            coins -= 100;
            goldBag += 1;
            OnGoldBagUpdated?.Invoke(goldBag);
        }
    }

    public void removeMoney(int _goldBag){
        goldBag -= _goldBag;
        OnGoldBagUpdated?.Invoke(goldBag);
    }

    
}
