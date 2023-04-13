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
            DontDestroyOnLoad(this.gameObject);
        }
    //Singleton

    public int addGoldBagOnWaveStart = 0;
    public int addCoinsOnSecond = 0;
    public int addArbaletteDamages = 0;
    public int addMaxGoldLimit = 0;
    public int addCoinsOnKill = 0;
    public int addCoinsOnArbaletteHit = 0;


    public enum UpgradeType{
        CardUpgrade,
        GlobalUpgrade,
        NewCard,
        StarterPack,
    }

    private void Start() {
        UpgradeUI.Instance.getStartersPack();
    }

    // private List<SOCard> getAllCards(){
    //     return new List<SOCard>(Resources.LoadAll<SOCard>(""));
    // }

    // private SOCard getRandomCard(){
    //     int randomIndex = Random.Range(0, allCards.Count);
    //     return allCards[randomIndex];
    // }

    // private SOCard getCardAtIndex(int _index){
    //     return allCards[_index];
    // }

    // private List<SOCard> getRdnStartCards(int _number){
    //     List<SOCard> startCards = new List<SOCard>();
    //     int[] rdnNumberList =  GetUniqueRandomNumbers(_number, 0, allCards.Count);
    //     foreach (int rdnNumber in rdnNumberList)
    //     {
    //         startCards.Add(getCardAtIndex(rdnNumber));
    //     }
    //     return startCards;
    // }

    // private void Start() {
        // getRdnStartCards(3).ToArray();
        // int[] numbers = GetUniqueRandomNumbers(3, 0, 10);
        // foreach (int number in numbers)
        // {
        //     Debug.Log(" num -> " + number);
        // }
    // }

    // private void getStartersPack(){
    //     UpgradeUI.Instance.AddUpgradeBox("1 deck", "Ajoute 3 cards dans votre deck", getRdnStartCards(3).ToArray(), UpgradeType.StarterPack, null);
    //     UpgradeUI.Instance.AddUpgradeBox("2 deck", "Ajoute 3 cards dans votre deck", getRdnStartCards(3).ToArray(), UpgradeType.StarterPack, null);
    //     UpgradeUI.Instance.AddUpgradeBox("3 deck", "Ajoute 3 cards dans votre deck", getRdnStartCards(3).ToArray(), UpgradeType.StarterPack, null);
    // }



    // public void addSelectedCardToDeck(){
    //     SOCard randomCard = getRandomCard();
    //     DeckManager.Instance.addCardToDeck(randomCard);
    // }


    // public static int[] GetUniqueRandomNumbers(int _number, int _min, int _max) {
    //     List<int> numbers = new List<int>();
    //     int ejectCount = 0;
    //     while (numbers.Count < _number) {
    //         int num = Random.Range(_min, _max);
    //         Debug.Log("GetUniqueRandomNumbers chosen: " + num);

    //         if (!numbers.Contains(num)) {
    //             Debug.Log("GetUniqueRandomNumbers added: " + num);
    //             numbers.Add(num);
    //         }
    //         ejectCount++;
    //         if (ejectCount >= 30) {
    //             Debug.Log("GetUniqueRandomNumbers: Ejecting after 30 tries");
    //             break;
    //         }
    //     }
    //     return numbers.ToArray();
    // }


}

public class RandomNumbers {
    
}
