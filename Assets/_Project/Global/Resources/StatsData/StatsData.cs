using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatsData", menuName = "Global/StatsData")]
public class StatsData : ScriptableObject
{
    public string difficulty;
    public int defaultMoney = 0;
}
