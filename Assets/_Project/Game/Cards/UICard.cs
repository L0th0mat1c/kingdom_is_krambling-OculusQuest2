using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICard : MonoBehaviour
{

    public int cost;
    private GameObject unit;
    [SerializeField]

    public TextMeshProUGUI UItitle;
    public TextMeshProUGUI UIcost;
    public TextMeshProUGUI UIdesc;
    public GameObject UImodel;

    public void setCardAttribute(SOCard card){
        UItitle.text = card.title.ToString();
        UIcost.text = card.cost.ToString();
        UIdesc.text = card.description.ToString();
        var model = Instantiate(card.model, UImodel.transform.position, Quaternion.identity);
        model.transform.SetParent(gameObject.transform);
    }

}
