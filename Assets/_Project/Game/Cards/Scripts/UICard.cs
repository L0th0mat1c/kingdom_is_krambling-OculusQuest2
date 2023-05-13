using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.XR.Interaction.Toolkit;

public class UICard : MonoBehaviour
{

    private SOCard card;
    public int cost;
    public GameObject unit {get; private set;}
    private Rigidbody rb;
    private int index;
    private bool isPlayable = false;

    private Vector3 initScale;

    public TextMeshProUGUI UItitle;
    public TextMeshProUGUI UIcost;
    public TextMeshProUGUI UIdesc;
    public GameObject UImodel;
    public TextMeshProUGUI UIdamage;
    public TextMeshProUGUI UIhealth;
    public TextMeshProUGUI UIspeed;
    private XRGrabInteractable grabInteractable;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        initScale = transform.localScale;
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable() {
        grabInteractable.activated.AddListener(PlayCard);
    }

    private void Start() {
        transform.DOScale(0, 0f);
        transform.DOScale(initScale, 0.5f);
        UpdateCardPayable(GoldManager.Instance.goldBag);
        GoldManager.Instance.OnGoldBagUpdated += UpdateCardPayable;
        GameManager.Instance.onGameStateChanged += onGameStateChanged;
        DeckManager.Instance.onDeleteAllCards += DestroyCard;
    }

    private void onGameStateChanged(GameManager.GameState _gameState){
        if(_gameState != GameManager.GameState.Upgrade && !isPlayable){
            DestroyCard();
        }
    }

    private void UpdateCardPayable(int _goldBag){
        if(cost <= _goldBag){
            isPlayable = true;
        }else{
            isPlayable = false;
        }
    }

    public void onSelectEnter(){
        rb.constraints = RigidbodyConstraints.None;
        DeckManager.Instance.cardIsSelected(index);
    }

    public void onSelectExit(){
        DeckManager.Instance.cardIsUnselected(index);
        DestroyCard();
    }

    public void onHoverEnter(){
        transform.DOScale(new Vector3(initScale.x + 0.006f, initScale.y + 0.006f ,initScale.z + 0.006f ), 0.1f);
    }

    public void onHoverExit(){
        transform.DOScale(initScale, 0.2f);
    }

    public bool isPlayableCard() {
        return isPlayable;
    }

    public void setCardAttribute(SOCard _card, int _index, bool _isPlayable = true){
        card = _card;
        cost = _card.getCardCost();
        UItitle.text = _card.title.ToString();
        UIcost.text = _card.getCardCost().ToString();
        UIdesc.text = _card.description.ToString();
        UIdamage.text = _card.getCardUnitDamage().ToString();
        UIhealth.text = _card.getCardUnitHealth().ToString();
        UIspeed.text = _card.getCardUnitSpeed().ToString();
        var model = Instantiate(_card.model, UImodel.transform.position, this.gameObject.transform.rotation);
        model.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        model.transform.SetParent(gameObject.transform);
        index = _index;
        unit = _card.unit;
        isPlayable = _isPlayable;
        //Debug.Log("new Card atributed");
        if(!_isPlayable){
            grabInteractable.enabled = false;
        }
    }

    private void PlayCard(ActivateEventArgs args){
        if(!isPlayable || !XRRayManager.Instance.isInPlayableZone) return;
        DeckManager.Instance.cardIsUnselected(index);
        //var xRRayInteractor = args.interactorObject.transform.GetComponent<XRRayInteractor>();
        //xRRayInteractor.TryGetHitInfo(out Vector3 position, out Vector3 normal, out int positionInLine, out bool isValidTarget);
        GameObject reticleInfos = XRRayManager.Instance.currentReticleUnit;
        var unitInstance = Instantiate(unit, reticleInfos.transform.position, reticleInfos.transform.rotation);
        GoldManager.Instance.removeMoney(cost);
        Destroy(gameObject);
    }

    public void DestroyCard(){
        transform.DOScale(0, 0.5f);
        Destroy(gameObject, 0.5f);
    }
    

    private void OnDestroy() {
        GoldManager.Instance.OnGoldBagUpdated -= UpdateCardPayable;
        GameManager.Instance.onGameStateChanged -= onGameStateChanged;
        DeckManager.Instance.onDeleteAllCards -= DestroyCard;
    }

}
