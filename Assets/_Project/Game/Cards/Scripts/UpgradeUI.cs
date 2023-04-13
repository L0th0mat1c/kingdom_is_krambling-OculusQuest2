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
            DontDestroyOnLoad(this.gameObject);
            allCards = getAllCards();
        }
    //Singleton


    [SerializeField]
    private TextMeshProUGUI mainTitle;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private GameObject upgardeBox;
   
    private List<SOCard> allCards;


    

    public void getStartersPack(){
        mainTitle.text = "Choisissez votre deck de départ";
        UpgradeUI.Instance.AddUpgradeBox("Deck 1", "Ajoute 3 cards dans votre deck", getRdnStartCards(3).ToArray(), UpgradeManager.UpgradeType.StarterPack, null);
        UpgradeUI.Instance.AddUpgradeBox("Deck 2", "Ajoute 3 cards dans votre deck", getRdnStartCards(3).ToArray(), UpgradeManager.UpgradeType.StarterPack, null);
        UpgradeUI.Instance.AddUpgradeBox("Deck 3", "Ajoute 3 cards dans votre deck", getRdnStartCards(3).ToArray(), UpgradeManager.UpgradeType.StarterPack, null);
    }

    public void AddUpgradeBox(string title, string description, SOCard[] cards, UpgradeManager.UpgradeType upgradeType, GameObject model){
        GameObject newUpdgradeBox = Instantiate(upgardeBox, content.transform);
        newUpdgradeBox.GetComponent<UpgradeBoxUI>().setProperty(title, description, cards, upgradeType, model);
        content.transform.SetParent(newUpdgradeBox.transform);
    }

    private List<SOCard> getAllCards(){
        return new List<SOCard>(Resources.LoadAll<SOCard>(""));
    }

    private SOCard getRandomCard(){
        int randomIndex = Random.Range(0, allCards.Count);
        return allCards[randomIndex];
    }

    private SOCard getCardAtIndex(int _index){
        return allCards[_index];
    }

    private List<SOCard> getRdnStartCards(int _number){
        List<SOCard> startCards = new List<SOCard>();
        
        int[] rdnNumberList =  GetUniqueRandomNumbers(_number, 0, allCards.Count);
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
