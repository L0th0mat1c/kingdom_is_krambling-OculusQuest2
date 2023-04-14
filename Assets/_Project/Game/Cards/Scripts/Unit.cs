using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Unit : MonoBehaviour
{
    private float health;
    private float speed;
    private float damage;

    private Vector3 initScale;

    private void Awake() {
        initScale = transform.localScale;
    }

    public void setUnitAttribute(SOCard card){
        damage = card.getCardUnitDamage();
        health = card.getCardUnitHealth();
        speed = card.getCardUnitSpeed();
    }

    private void Start() {
        transform.localScale = Vector3.zero;
        transform.DOScale(initScale, 0.5f);
    }
}
