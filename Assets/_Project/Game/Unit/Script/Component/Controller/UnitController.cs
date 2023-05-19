using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [Header("Stats")]
    public int HP = 10;
    public int Attack = 0;
    public float RangeDetection = 1;
    public float RangeAttack = 1;

    [Header("Units components")]
    public BaseUnitBehaviour Behaviour;

    private void OnDestroy()
    {
        onUnitDie();
    }

    public virtual void AttackUnit(UnitController unit)
    {
        if(Attack > 0)
            unit.ReceiveDamage(Attack);
    }

    public virtual void ReceiveDamage(int damage)
    {
        //Show Damage Popup
        DamagePopupManager.Instance.newDamagePopup(gameObject.transform, damage);
        //Sound
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        if(audio != null)
            audio.Play();
        //Damage
        HP -= damage;
        if (HP <= 0)
            Destroy(gameObject);
    }

    protected virtual void onUnitDie()
    {
        UnitEvent.UnitDie(this);
    }
}
