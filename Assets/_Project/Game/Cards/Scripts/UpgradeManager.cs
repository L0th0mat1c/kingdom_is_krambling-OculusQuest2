using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpgradeManager : MonoBehaviour
{

    //Singleton
        private static UpgradeManager instance = null;
        public static UpgradeManager Instance => instance;
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

    // arbalette 
    public int addArbaletteDamages = 0;
    public int addArbaletteSpeed = 0;
    public int addCoinsOnArbaletteHit = 0;

    // coins
    public int addCoinsOnKill = 0;
    public int addGoldBagOnWaveStart = 0;
    public int addCoinsOnSecond = 0;

    public enum UpgradeType{
        CardUpgrade,
        GlobalUpgrade,
        NewCard,
        StarterPack,
    }

    private void Start() {
        UpgradeUI.Instance.setTitle("Choisissez votre deck de départ");
        UpgradeUI.Instance.getStartersPack();
        GameManager.Instance.onGameStateChanged += onGameStateChanged;
    }

    private void onGameStateChanged(GameManager.GameState _gameState){
        if(_gameState == GameManager.GameState.Upgrade){
            UpgradeUI.Instance.setTitle("Choisissez votre upgrade");
            displayNewUpgrades();
        }else{
            UpgradeUI.Instance.setTitle("");
        }
    }
            
    private void displayNewUpgrades(){
        UpgradeUI.Instance.getBasicUpgradePack();
    }




}

public class RandomNumbers {
    
}
