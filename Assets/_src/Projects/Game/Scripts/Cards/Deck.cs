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
    private GameObject cardTemplate;
    // Start is called before the first frame update
    void Start()
    {
        generateCardInDeck();
    }

    public void generateCardInDeck(){
        int index = 0;
        foreach (SOCard card in deck)
        {
            GameObject cardInstance = Instantiate(cardTemplate, new Vector3((index * 5) + 5, 0, 0), Quaternion.identity);
            cardInstance.GetComponent<UICard>().setCardAttribute(card);
            index++;
        }
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
