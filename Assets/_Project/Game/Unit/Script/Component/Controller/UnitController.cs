using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    protected const float WidthHealtBarSize = 1f;
    protected const float HeightHealtBarSize = 0.2f;

    [Header("Stats")]
    public int HP = 10;
    public int MaxHP;
    public int Attack = 0;
    public float RangeDetection = 1;
    public float RangeAttack = 1;
    public int Weight = 1;

    [Header("Units components")]
    public BaseUnitBehaviour Behaviour;
    protected MMHealthBar healthBar;

    private void Awake()
    {
        InitController();
        InitHealthBar();
    }

    private void OnDestroy()
    {
        onUnitDie();
    }

    protected virtual void InitController() { }

    protected virtual void InitHealthBar()
    {
        // Data
        MaxHP = HP;
        healthBar = GetComponent<MMHealthBar>();
        updateHealthBar();

        // Size
        healthBar.Size.x = WidthHealtBarSize / transform.localScale.x;
        healthBar.Size.y = HeightHealtBarSize / transform.localScale.y;

        // Position
        healthBar.HealthBarOffset.y = 1.4f;
    }

    public virtual void AttackUnit(UnitController unit)
    {
        if(Attack > 0)
            unit.ReceiveDamage(Attack);
    }

    public virtual void ReceiveDamage(int damage)
    {
        //Show Damage Popup for Ally or Ennemy
        Color colors = isAlly() ? ColorManager.Instance.allyTextColor : ColorManager.Instance.ennemyTextColor;
        DamagePopupManager.Instance.newDamagePopup(gameObject.transform, damage, colors);

        //Sound
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        if(audio != null)
            audio.Play();
        //Damage
        HP -= damage;
        updateHealthBar();
        if (HP <= 0)
            Destroy(gameObject);
    }

    /// <summary>
    /// Destroy this unit.
    /// </summary>
    public virtual void Destroy()
    {
        HP = 0;
        updateHealthBar();
        Destroy(gameObject);
    }
    
    public bool isAlly()
    {
        if(gameObject.tag == "EnemyUnit"){
            return false;
        }
        return true;
    }

    protected virtual void onUnitDie()
    {
        UnitEvent.UnitDie(this);
    }

    private void updateHealthBar()
    {
        bool healthBarIsDisplay = HP > 0;
        healthBar.UpdateBar(HP, 0, MaxHP, healthBarIsDisplay);
    }
}
