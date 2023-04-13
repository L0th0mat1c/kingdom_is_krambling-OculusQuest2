using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeckManager : MonoBehaviour
{
    
    //Singleton
        private static DeckManager instance = null;
        public static DeckManager Instance => instance;
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

    public event Action onDeleteAllCards;

   

    public event Action<int> onCardSelected;
    public event Action<int> onCardPlayed;

    public List<SOCard> deck;

    [SerializeField]
    public int maxCardsOnBoard = 10;
    [SerializeField]
    public GameObject cardTemplate;
    [SerializeField]
    private Transform[] cardSlots;

    void Start()
    {
        onCardPlayed += updateDeck;
        generateCardInDeck();
    }

    public void cardIsSelected(int _index){
        onCardSelected?.Invoke(_index);
    }

    public void cardIsPlayed(int _index){
        onCardPlayed?.Invoke(_index);
    }

    public void cardIsUnselected(int _index){
        updateDeck(_index);
    }


    public void addCardToDeck(SOCard _card){
        deck.Add(_card);
        generateCardInDeck();
    }

    public void addMultipleCardsToDeck(SOCard[] _cards){
        foreach (SOCard card in _cards)
        {
            deck.Add(card);
        }
        generateCardInDeck();
    }

    public void upgradeCardInDeck(int _index, SOCard _card){
        deck[_index] = _card;
        generateCardInDeck();
    }


    private void updateDeck(int _index){
        float offset = 0;
        if(deck.Count%2 == 0){
            offset = 0.11f;
        }
        var slotPosition = cardSlots[_index].transform.position;
        slotPosition.x -= offset;
        var cardInstance = Instantiate(cardTemplate, slotPosition, Quaternion.identity);
        var card = deck[_index];
        cardInstance.GetComponent<UICard>().setCardAttribute(card, _index);
    }

    public void generateCardInDeck(){
        onDeleteAllCards?.Invoke();
        float offset = 0;
        if(deck.Count%2 == 0){
            offset = 0.11f;
        }
        int index = 0;
        foreach (SOCard card in deck)
        {   
            if(index >= maxCardsOnBoard) return;
            var slotPosition = cardSlots[index].transform.position;
            slotPosition.x -= offset;
            GameObject cardInstance = Instantiate(cardTemplate, slotPosition, Quaternion.identity);
            cardInstance.GetComponent<UICard>().setCardAttribute(card, index);
            index++;
        }
    } 
}
