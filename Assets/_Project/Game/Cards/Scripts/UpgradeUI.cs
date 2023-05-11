using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    //Singleton
        private static UpgradeUI instance = null;
        public static UpgradeUI Instance => instance;
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
            allAvailableCards = getallAvailableCards();
        }
    //Singleton


    [SerializeField]
    private TextMeshProUGUI mainTitle;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private GameObject upgardeBox;
   
    private List<SOCard> allAvailableCards;

    [SerializeField]
    private GameObject arbaletModel;
    [SerializeField]
    private GameObject goldModel;


    public void removeAvailableCard(SOCard _card){
        allAvailableCards.Remove(_card);
    }

    public void getStartersPack(){
        AddUpgradeBox("Deck 1", "Ajoute 3 cards dans votre deck", getRdnStartCards(3).ToArray(), UpgradeManager.UpgradeType.StarterPack, null);
        AddUpgradeBox("Deck 2", "Ajoute 3 cards dans votre deck", getRdnStartCards(3).ToArray(), UpgradeManager.UpgradeType.StarterPack, null);
        AddUpgradeBox("Deck 3", "Ajoute 3 cards dans votre deck", getRdnStartCards(3).ToArray(), UpgradeManager.UpgradeType.StarterPack, null);
    }

    public void getBasicUpgradePack(){
        instNewCard();
        instCardUpgrade();
        instGlobalUpgrade();
    }

    public void setTitle(string _title){
        mainTitle.text = _title;
    }

    private void instCardUpgrade(){
        List<SOCard> deck = DeckManager.Instance.getDeck();
        SOCard cardBase = deck[Random.Range(0, deck.Count)];
        SOCard CardUpgrade = cardBase.getNextLvl();
        SOCard[] upgradeList = new SOCard[2];
        upgradeList[0] = cardBase;
        upgradeList[1] = CardUpgrade;
        AddUpgradeBox("Amélioration de carte", "Améliore cette carte de votre deck", upgradeList, UpgradeManager.UpgradeType.CardUpgrade, null);
    }

    private void instNewCard(){
        if(allAvailableCards.Count <= 0 || DeckManager.Instance.getDeck().Count >= 9) return;
        SOCard newCard = allAvailableCards[Random.Range(0, allAvailableCards.Count)];
        SOCard[] upgradeList = new SOCard[1];
        upgradeList[0] = newCard;
        AddUpgradeBox("Nouvelle carte", "Ajoute une nouvelle carte a votre deck", upgradeList, UpgradeManager.UpgradeType.NewCard, null);
    }

    private void instGlobalUpgrade(){
        switch(Random.Range(0, 6)){
            case 0:
                AddUpgradeBox("Gold start", "Commence chaque vagues avec +2 gold", null, UpgradeManager.UpgradeType.GlobalUpgrade, goldModel, 0);
                break;
            case 1:
                AddUpgradeBox("Arbalette damages", "Agmente les dégat à l'arbalète", null, UpgradeManager.UpgradeType.GlobalUpgrade, arbaletModel, 1);
                break;
            case 2:
                AddUpgradeBox("Arbalette speed", "Augmente la cadence de tir de l'arbalète.", null, UpgradeManager.UpgradeType.GlobalUpgrade, arbaletModel, 2);
                break;
            case 3:
                AddUpgradeBox("Coins per second", "Ajoute +5 pieces par seconde", null, UpgradeManager.UpgradeType.GlobalUpgrade, goldModel, 3);
                break;
            case 4:
                AddUpgradeBox("Coins per kill", "Ajoute +5 pieces par kill", null, UpgradeManager.UpgradeType.GlobalUpgrade, goldModel, 4);
                break;
            case 5:
                AddUpgradeBox("Coin per arbalette hit", "Ajoute +1 piece par hit de l'arbalète", null, UpgradeManager.UpgradeType.GlobalUpgrade, arbaletModel, 5);
                break;
        }
    }

    public void AddUpgradeBox(string title, string description, SOCard[] cards, UpgradeManager.UpgradeType upgradeType, GameObject model, int value = 0){
        GameObject newUpdgradeBox = Instantiate(upgardeBox, content.transform);
        newUpdgradeBox.GetComponent<UpgradeBoxUI>().setProperty(title, description, cards, upgradeType, model, value);
        content.transform.SetParent(newUpdgradeBox.transform);
    }

    private List<SOCard> getallAvailableCards(){
        return new List<SOCard>(Resources.LoadAll<SOCard>(""));
    }

    private SOCard getRandomCard(){
        int randomIndex = Random.Range(0, allAvailableCards.Count);
        return allAvailableCards[randomIndex];
    }

    private SOCard getCardAtIndex(int _index){
        return allAvailableCards[_index];
    }

    private List<SOCard> getRdnStartCards(int _number){
        List<SOCard> startCards = new List<SOCard>();
        
        int[] rdnNumberList =  GetUniqueRandomNumbers(_number, 0, allAvailableCards.Count);
        foreach (int rdnNumber in rdnNumberList)
        {
            startCards.Add(getCardAtIndex(rdnNumber));
        }
        return startCards;
    }


    public static int[] GetUniqueRandomNumbers(int _number, int _min, int _max) {
        List<int> numbers = new List<int>();
        int ejectCount = 0;
        while (numbers.Count < _number) {
            int num = Random.Range(_min, _max);

            if (!numbers.Contains(num)) {
                numbers.Add(num);
            }
            ejectCount++;
            if (ejectCount >= 30) {
                break;
            }
        }
        return numbers.ToArray();
    }



    
}
