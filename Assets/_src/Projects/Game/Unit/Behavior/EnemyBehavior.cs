using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public SphereCollider Collider;

    private Vector3 castlePosition;
    private NavMeshAgent agent;

    private void Start()
    {
        GameObject castle = GameObject.Find("Castle");

        if (castle == null)
            throw new System.Exception("Castle not found");

        castlePosition = castle.transform.position;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(castlePosition);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerUnit")
            agent.SetDestination(other.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerUnit")
            agent.SetDestination(castlePosition);
    }
}
