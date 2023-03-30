using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public int HP;
    public float Range;

    private void Start()
    {
        addTriggerZone();
    }

    private void addTriggerZone()
    {
        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = Range;
    }
}
