using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOCard : ScriptableObject
{
    
    public  enum Type
    {
        invocation,
        spell,
    }
    public Type type;

    public enum Lvl
    {
        basic,
        upgrade1,
        upgrade2,
        upgrade3,
    }
    public Lvl lvl = Lvl.basic;

    public enum Behaviour
    {
        Default,
        NoBehaviour,
        Bomb
    }
    public Behaviour behaviour = Behaviour.Default;

    [Header("CardUI")]
    public GameObject model;
    public string title;
    public string description;

    [Header("CardStats")]
    public LvlAmount cost;
    public LvlAmount unitDamage;
    public LvlAmount unitHealth;
    public LvlAmount unitSpeed;
    public LvlAmount unitRangeDetection;
    public LvlAmount unitRangeAttack;
    public GameObject unit;

    public int getCardCost(){
        return getCardState(cost);
    }
    public int getCardUnitDamage(){
        return getCardState(unitDamage);
    }
    public int getCardUnitHealth(){
        return getCardState(unitHealth);
    }
    public int getCardUnitSpeed(){
        return getCardState(unitSpeed);
    }
    public int getCardUnitRangeDetection(){
        return getCardState(unitRangeDetection);
    }
    public int getCardUnitRangeAttack()
    {
        return getCardState(unitRangeAttack);
    }


    public SOCard getNextLvl(){
        switch(lvl){
            case Lvl.basic:
                lvl = Lvl.upgrade1;
                break;
            case Lvl.upgrade1:
                lvl = Lvl.upgrade2;
                break;
            case Lvl.upgrade2:
                lvl = Lvl.upgrade3;
                break;
            default:
                lvl = Lvl.upgrade3;
                break;
        }
        return this;
    }
    
    
    private int getCardState(LvlAmount lvlAmount){
        switch(lvl){
            case Lvl.basic:
                return lvlAmount.basic;
            case Lvl.upgrade1:
                return lvlAmount.basic + lvlAmount.addUpgrade1;
            case Lvl.upgrade2:
                return lvlAmount.basic + lvlAmount.addUpgrade1 + lvlAmount.addUpgrade2;
            default:
                return lvlAmount.basic + lvlAmount.addUpgrade1 + lvlAmount.addUpgrade2 + lvlAmount.addUpgrade3;
        }
    }
}

[System.Serializable]
public struct LvlAmount
{
    public int basic;
    public int addUpgrade1;
    public int addUpgrade2;
    public int addUpgrade3;
}
