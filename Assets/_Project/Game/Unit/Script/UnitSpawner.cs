using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject unitPrefab;
    private WaveManager waveManager; 
    private GameManager gameManager;
    private static int staticIndex = 0;
    private int index;

    void Start()
    {
        waveManager = WaveManager.Instance;
        gameManager = GameManager.Instance;
        gameManager.onGameStateChanged += onGameStateChanged;
        index = staticIndex;
        staticIndex++;
    }

    private void onGameStateChanged(GameManager.GameState _gameState){
        if(_gameState == GameManager.GameState.Combat){
            Debug.Log("index:"+ index);
            InvokeRepeating("invokeEnemy", index * waveManager.timeBetweenSpawn + 2, waveManager.timeBetweenSpawn);
        }else if(_gameState != GameManager.GameState.Combat){
            CancelInvoke("invokeEnemy");
        }
    }
    
    private void invokeEnemy()
    {
        if(waveManager.isMaxEnemies() || waveManager.stopSpowning){
            return;
        }
        var weight = unitPrefab.GetComponent<EnemyUnitController>().Weight;
        waveManager.addEnemy(weight);
        GameObject unit = Instantiate(unitPrefab, transform.position, transform.rotation);
    }
}
