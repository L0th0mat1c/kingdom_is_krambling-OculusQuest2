using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private GameObject castle;
    private NavMeshAgent agent;

    private void Start()
    {
        castle = GameObject.Find("Castle");
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(castle.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
