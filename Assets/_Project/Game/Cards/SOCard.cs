using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOCard : ScriptableObject
{
    
    public  enum TypeList
    {
        invocation,
        spell,
    }
    public TypeList type;
    public GameObject model;
    public int cost;
    public string title;
    public string description;
    public GameObject unit;
}
