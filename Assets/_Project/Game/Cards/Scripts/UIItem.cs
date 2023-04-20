using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.XR.Interaction.Toolkit;

public class UIItem : MonoBehaviour
{


   private Vector3 initScale;

    private void Start() {
        initScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(initScale, 0.5f);
        GameManager.Instance.onGameStateChanged += onGameStateChanged;
    }

    private void onGameStateChanged(GameManager.GameState _gameState){
        if(_gameState != GameManager.GameState.Upgrade){
            DestroyItem();
        }
    }


    public void DestroyItem(){
        transform.DOScale(0, 0.5f);
        Destroy(gameObject, 0.5f);
    }
    

    private void OnDestroy() {
        GameManager.Instance.onGameStateChanged -= onGameStateChanged;
    }

}
