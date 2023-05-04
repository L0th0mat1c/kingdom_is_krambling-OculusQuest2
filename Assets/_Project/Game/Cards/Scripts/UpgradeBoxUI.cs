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
    private CanvasGroup canvasGroup;

    private Vector3 initPos;
    private Vector3 initScale;

    // [SerializeField]
    // private Sprite upgradeCardSprite;
    [SerializeField]
    private GameObject upgradeCardSprite;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    private void Start() {
        // get
        initScale = transform.localScale;
        // initPos = transform.position;
        // init 
        transform.localScale = Vector3.zero;
        canvasGroup.DOFade(0,0f);
        transform.DOLocalMoveY(1,0f);
        // animate
        transform.DOScale(1,0.5f);
        canvasGroup.DOFade(0.7f,0.5f);
        transform.DOLocalMoveY(0,0.5f);
        // set
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
        value = _value;
        model = _model;
        StartCoroutine(addItemsToSlotsCoroutine());
    }

    private void addAllCardsToSlots(){
        var index = 0;
        foreach(SOCard card in cards){
            var slotPosition = slots[index].transform.position;
            GameObject cardInstance = Instantiate(DeckManager.Instance.cardTemplate, new Vector3(slotPosition.x, slotPosition.y, slotPosition.z - 0.1f) , Quaternion.identity);
            cardInstance.GetComponent<UICard>().setCardAttribute(card, index, false);
            index++;
        }
    }

    private void addItemToSlot(GameObject _item, int _index){
        var slotPosition = slots[_index].transform.position;
        Instantiate(_item, new Vector3(slotPosition.x, slotPosition.y, slotPosition.z - 0.1f) , Quaternion.identity);
    }

    private void addCardToSlot(SOCard _card, int _index){
        var slotPosition = slots[_index].transform.position;
        GameObject cardInstance = Instantiate(DeckManager.Instance.cardTemplate, new Vector3(slotPosition.x, slotPosition.y, slotPosition.z - 0.1f) , Quaternion.identity);
        cardInstance.GetComponent<UICard>().setCardAttribute(_card, _index, false);
    }

    private IEnumerator addItemsToSlotsCoroutine(){
        yield return new WaitForSeconds(0.4f);
        if(upgradeType == UpgradeManager.UpgradeType.StarterPack){
            addAllCardsToSlots();
        }else if(upgradeType == UpgradeManager.UpgradeType.CardUpgrade){
            addCardToSlot(cards[0], 0);
            upgradeCardSprite.SetActive(true);
            addCardToSlot(cards[0], 2);
        }else if(upgradeType == UpgradeManager.UpgradeType.NewCard){
            addCardToSlot(cards[0], 1);
        }else if(upgradeType == UpgradeManager.UpgradeType.GlobalUpgrade){
            addItemToSlot(model, 1);
        }
    }   

    public void onHooverEnter(){
        if(GameManager.Instance.gameState == GameManager.GameState.Upgrade)
            transform.DOScale(1.05f, 0.1f);
            transform.DOLocalMoveZ(-0.08f, 0.1f);
            canvasGroup.DOFade(1,0.1f);
    }

    public void onHooverExit(){
        if(GameManager.Instance.gameState == GameManager.GameState.Upgrade)
            transform.DOScale(initScale, 0.3f);
            transform.DOLocalMoveZ(0f, 0.3f);
            canvasGroup.DOFade(0.7f,0.3f);
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
                foreach(SOCard card in cards){
                    UpgradeUI.Instance.removeAvailableCard(card);
                }
                break;
            case UpgradeManager.UpgradeType.NewCard:
                DeckManager.Instance.addCardToDeck(cards[0]);
                UpgradeUI.Instance.removeAvailableCard(cards[0]);
                break;
            case UpgradeManager.UpgradeType.CardUpgrade:
                DeckManager.Instance.upgradeCardInDeck(cards[0]);
                break;
            case UpgradeManager.UpgradeType.GlobalUpgrade:
                addGlobalUpgrade();
                break;
        }
    }

     // arbalette 
    public int addArbaletteDamages = 0;
    public int addArbaletteSpeed = 0;
    public int addCoinsOnArbaletteHit = 0;

    // coins
    public int addCoinsOnKill = 0;
    public int addGoldBagOnWaveStart = 0;
    public int addCoinsOnSecond = 0;

    private void addGlobalUpgrade(){
        switch(value){
            case 0:
                // AddUpgradeBox("Gold start", "Commence chaque vagues avec +2 gold", null, UpgradeManager.UpgradeType.GlobalUpgrade, goldModel, 0);
                UpgradeManager.Instance.addGoldBagOnWaveStart += 2;
                break;
            case 1:
                // AddUpgradeBox("Arbalette damages", "Agmente les dégat à l'arbalète", null, UpgradeManager.UpgradeType.GlobalUpgrade, arbaletModel, 1);
                UpgradeManager.Instance.addArbaletteDamages += 5;
                break;
            case 2:
                // AddUpgradeBox("Arbalette speed", "Augmente la cadence de tir de l'arbalète.", null, UpgradeManager.UpgradeType.GlobalUpgrade, arbaletModel, 2);
                UpgradeManager.Instance.addArbaletteSpeed += 5;
                break;
            case 3:
                // AddUpgradeBox("Coins per second", "Ajoute +5 pieces par seconde", null, UpgradeManager.UpgradeType.GlobalUpgrade, goldModel, 3);
                UpgradeManager.Instance.addCoinsOnSecond += 5;
                break;
            case 4:
                // AddUpgradeBox("Coins per kill", "Ajoute +5 pieces par kill", null, UpgradeManager.UpgradeType.GlobalUpgrade, goldModel, 4);
                UpgradeManager.Instance.addCoinsOnSecond += 5;
                break;
            case 5:
                // AddUpgradeBox("Coin per arbalette hit", "Ajoute +1 piece par hit de l'arbalète", null, UpgradeManager.UpgradeType.GlobalUpgrade, arbaletModel, 5);
                UpgradeManager.Instance.addCoinsOnArbaletteHit += 1;
                break;
        }
    }


}
