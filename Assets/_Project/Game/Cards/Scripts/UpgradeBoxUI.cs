using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using MoreMountains.Tools;

public class UpgradeBoxUI : MonoBehaviour
{
    

    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField]
    private GameObject[] slots;


    private SOCard[] cards;
    private UpgradeManager.UpgradeType upgradeType;
    private GameObject model;
    private int value;


    private Vector3 initScale;
    
    private void Start() {
        initScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(1,0.5f);
        GameManager.Instance.onGameStateChanged += onGameStateChanged;
    }

    private void onGameStateChanged(GameManager.GameState _gameState){
        if(_gameState != GameManager.GameState.Upgrade){
            DestroyUpgradeBox();
        }
    }


    public void setProperty(string _title, string _description, SOCard[] _cards, UpgradeManager.UpgradeType _upgradeType, GameObject _model = null, int _value = 0){
        upgradeType = _upgradeType;
        title.text = _title;
        description.text = _description;  
        cards = _cards;
        StartCoroutine(addCardsToSlotsCoroutine());
    }

    private void addCardsToSlots(){
        var index = 0;
        foreach(SOCard card in cards){
            var slotPosition = slots[index].transform.position;
            GameObject cardInstance = Instantiate(DeckManager.Instance.cardTemplate, new Vector3(slotPosition.x, slotPosition.y, slotPosition.z - 0.1f) , Quaternion.identity);
            cardInstance.GetComponent<UICard>().setCardAttribute(card, index, false);
            index++;
        }
    }

    private IEnumerator addCardsToSlotsCoroutine(){
        yield return new WaitForSeconds(1f);
        addCardsToSlots();
    }   

    public void onHooverEnter(){
        if(GameManager.Instance.gameState == GameManager.GameState.Upgrade)
            transform.DOScale(1.1f, 0.1f);
    }

    public void onHooverExit(){
        if(GameManager.Instance.gameState == GameManager.GameState.Upgrade)
            transform.DOScale(initScale, 0.3f);
    }

    public void OnActivate(){
        addUpgrade();
    }

    public void DestroyUpgradeBox(){
        transform.DOScale(0, 0.5f);
        Destroy(gameObject, 0.5f);
    }

    private void OnDestroy() {
        GameManager.Instance.onGameStateChanged -= onGameStateChanged;
    }

    private void addUpgrade(){
        GameManager.Instance.changeGameState(GameManager.GameState.Combat);
        switch(upgradeType){
            case UpgradeManager.UpgradeType.StarterPack:
                DeckManager.Instance.addMultipleCardsToDeck(cards);
                break;
            case UpgradeManager.UpgradeType.NewCard:
                DeckManager.Instance.addCardToDeck(cards[0]);
                break;
            case UpgradeManager.UpgradeType.CardUpgrade:
                DeckManager.Instance.upgradeCardInDeck(value, cards[0]);
                break;
            case UpgradeManager.UpgradeType.GlobalUpgrade:
                addGlobalUpgrade();
                break;
        }
    }

    private void addGlobalUpgrade(){
        //TODO
    }


}
