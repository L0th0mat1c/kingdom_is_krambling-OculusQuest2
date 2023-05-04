using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyUnitBuilder
{
    public int Health = 1;
    public int Damage = 0;
    public int Speed = 1;
    public int Range = 1;

    public IUnitAttack UnitAttack;
    public IUnitBehaviour UnitBehaviour;

    public void SetAttributes(SOCard card)
    {
        Health = card.getCardUnitHealth();
        Damage = card.getCardUnitDamage();
        Speed = card.getCardUnitSpeed();
        Range = card.getCardUnitRange();

        if (card.DoDamage)
            UnitAttack = new UnitAttack();
        else
            UnitAttack = null;

        UnitBehaviour = getBehaviour(card.behaviour);
    }

    public AllyUnitController BuildUnit()
    {
        AllyUnitController unitController = new AllyUnitController()
        {
            HP = Health,
            Attack = Damage,
            Range = Range
        };
        return unitController;
    }

    private IUnitBehaviour getBehaviour(SOCard.Behaviour behaviour)
    {
        IUnitBehaviour result;
        switch (behaviour)
        {
            case SOCard.Behaviour.Default:
                result = new AllyUnitBehaviour();
                break;
            default:
                result = new AllyUnitBehaviour();
                break;
        }
        return result;
    }
}
