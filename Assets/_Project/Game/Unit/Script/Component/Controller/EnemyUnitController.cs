using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitController : UnitController
{
    protected override void InitHealthBar()
    {
        base.InitHealthBar();

        // Apply color to the health bar
        Gradient foregroundGradient = new Gradient();
        Gradient delayedGradient = new Gradient();
        GradientColorKey[] foregroundColorKey = new GradientColorKey[2];
        GradientColorKey[] delayedColorKey = new GradientColorKey[2];

        foregroundColorKey[0].color = new Color() { r = 1, g = 24f / 255f, b = 0 };
        foregroundColorKey[1].color = new Color() { r = 1, g = 24f / 255f, b = 0 };
        delayedColorKey[0].color = new Color() { r = 1, g = 165f / 255f, b = 0 };
        delayedColorKey[1].color = new Color() { r = 1, g = 165f / 255f, b = 0 };

        foregroundColorKey[0].time = 0;
        foregroundColorKey[1].time = 1;
        delayedColorKey[0].time = 0;
        delayedColorKey[1].time = 1;

        foregroundGradient.colorKeys = foregroundColorKey;
        delayedGradient.colorKeys = delayedColorKey;

        healthBar.ForegroundColor = foregroundGradient;
        healthBar.DelayedColor = delayedGradient;
    }
}
