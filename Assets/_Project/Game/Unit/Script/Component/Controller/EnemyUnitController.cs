using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitController : UnitController
{
    private MMHealthBar healthBar;
    private int maxHealth;

    private void Awake()
    {
        maxHealth = HP;
        healthBar = GetComponent<MMHealthBar>();
        updateHealthBar(maxHealth);
    }

    public override void ReceiveDamage(int damage)
    {
        updateHealthBar(HP - damage);
        base.ReceiveDamage(damage);
    }

    private void updateHealthBar(int currentHp)
    {
        Debug.Log($"Current HP : {currentHp}");
        bool healthBarIsDisplay = currentHp > 0;
        healthBar.UpdateBar(currentHp, 0, maxHealth, healthBarIsDisplay);
    }
}
