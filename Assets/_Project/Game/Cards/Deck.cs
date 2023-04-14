using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    
    //Singleton
        private static Deck instance = null;
        public static Deck Instance => instance;
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
    

    public SOCard[] deck;

    [SerializeField]
    public int maxCardsOnBoard = 10;
    [SerializeField]
    private GameObject cardTemplate;
    [SerializeField]
    private Transform[] cardSlots;
    // Start is called before the first frame update
    void Start()
    {
        generateCardInDeck();
    }

    public void generateCardInDeck(){
        float offset = 0;
        if(deck.Length%2 == 0){
            offset = 1.25f;
        }
        int index = 0;
        foreach (SOCard card in deck)
        {   
            if(index >= maxCardsOnBoard) return;
            var slotPosition = cardSlots[index].transform.position;
            slotPosition.x -= offset;
            GameObject cardInstance = Instantiate(cardTemplate, slotPosition, Quaternion.identity);
            cardInstance.GetComponent<UICard>().setCardAttribute(card);
            index++;
        }
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
