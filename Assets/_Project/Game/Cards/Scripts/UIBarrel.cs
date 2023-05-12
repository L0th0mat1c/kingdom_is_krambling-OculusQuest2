using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using MoreMountains.Tools;

public class UIBarrel : MonoBehaviour
{

    public TextMeshProUGUI UIgoldBag;
    [SerializeField]
    private MMProgressBar progressBar;

    [SerializeField]
    private GameObject[] GoldItems;
    void Start()
    {
        GoldManager.Instance.OnGoldBagUpdated += UpdateUIBarrel;
        GoldManager.Instance.OnCoinUpdated += UpdateUICoinBar;
        setGoldAmount(0);
        progressBar.UpdateBar(0,0,100);
    }

    private void UpdateUIBarrel(int _goldBag){
        setGoldAmount(_goldBag);
    }

    private void UpdateUICoinBar(int _coin){
        progressBar.UpdateBar01(_coin / 100f);
        if(_coin >= 100){
            progressBar.UpdateBar01(0);
        }
    }

    private void setGoldAmount(int GoldAmount){
        UIgoldBag.text = GoldAmount.ToString();
        for(var i = 0; i < GoldItems.Length; i++){
            // if(i >= GoldAmount){
            //     GoldItems[i].transform.DOScale(0,0.2f);
            // }else{
            //     GoldItems[i].transform.DOScale(1,0.2f);
            // }
        }
    }
}
