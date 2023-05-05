using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyUnitBuilder
{
    public int Health = 1;
    public int Damage = 0;
    public int Speed = 1;
    public int RangeDetection = 1;
    public int RangeAttack = 1;

    public BaseUnitBehaviour UnitBehaviour;

    public void SetAttributes(SOCard card)
    {
        Health = card.getCardUnitHealth();
        Damage = card.getCardUnitDamage();
        Speed = card.getCardUnitSpeed();
        RangeDetection = card.getCardUnitRangeDetection();
        RangeAttack = card.getCardUnitRangeAttack();

        UnitBehaviour = getBehaviour(card.behaviour);
    }

    public AllyUnitController BuildUnit()
    {
        AllyUnitController unitController = new AllyUnitController()
        {
            HP = Health,
            Attack = Damage,
            RangeDetection = RangeDetection,
            RangeAttack = RangeAttack,
        };
        return unitController;
    }

    private BaseUnitBehaviour getBehaviour(SOCard.Behaviour behaviour)
    {
        BaseUnitBehaviour result;
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
