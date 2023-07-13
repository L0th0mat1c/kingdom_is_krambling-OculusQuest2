using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : BaseUnitBehaviour
{
    private List<Vector3> castlePositions = new List<Vector3>();
    private Vector3 castleDestination;

    protected override void Init()
    {
        List<CastleController> castles = FindObjectsOfType<CastleController>().ToList();

        foreach (CastleController castle in castles)
            if (castle.gameObject != null)
                castlePositions.Add(castle.gameObject.transform.position);

        if (controller.RangeAttack - 0.5f >= 1)
            agent.stoppingDistance = controller.RangeAttack - 0.5f;
        else
            agent.stoppingDistance = 1;

        if (castlePositions.Count > 0 && agent.isActiveAndEnabled)
        {
            castleDestination = findCloseCastle(gameObject.transform.position);
            agent.SetDestination(castleDestination);
        }
        InvokeRepeating("actualiserCastleList", 2f, 2f);
        base.Init();
    }

    private void actualiserCastleList()
    {
        castlePositions.Clear();
        List<CastleController> castles = FindObjectsOfType<CastleController>().ToList();
        foreach (CastleController castle in castles)
            if (castle.gameObject != null)
                castlePositions.Add(castle.gameObject.transform.position);
    }

    protected override void UpdateUnit()
    {
        if(GameManager.Instance.gameState != GameManager.GameState.Combat){
            Destroy(gameObject);
            return;
        }

        float closeUnitDistance = 0f;
        UnitController unitController = findCloseUnit(gameObject.transform.position, "PlayerUnit");
        // Si une unité proche est trouvé on calcule la distance
        if (unitController != null)
            closeUnitDistance = Vector3.Distance(gameObject.transform.position, unitController.gameObject.transform.position);
        
        // Gestion de la cible
        if (unitController != null && closeUnitDistance <= controller.RangeDetection)
        {
            if (closeUnitDistance > controller.RangeAttack && agent.destination != unitController.gameObject.transform.position)
                agent.SetDestination(unitController.gameObject.transform.position);

            if (closeUnitDistance <= controller.RangeAttack)
                controller.AttackUnit(unitController);
        }
        else if (castlePositions.Count > 0 && agent.destination != castleDestination)
        {
            setCastleDestination(gameObject.transform.position);
        }
        else {
            setDestinationToPlayerHealth();
        }
    }

    private Vector3 findCloseCastle(Vector3 unitPosition)
    {
        float closeUnitDistance = castlePositions.Min(u => Vector3.Distance(unitPosition, u));
        return castlePositions.Find(u => Vector3.Distance(unitPosition, u) == closeUnitDistance);
    }

    private void setCastleDestination(Vector3 unitPosition)
    {
        castleDestination = findCloseCastle(unitPosition);
        agent.SetDestination(castleDestination);
    }

    private void setDestinationToPlayerHealth() {
        float distanceFromHealth = Vector3.Distance(gameObject.transform.position, GameManager.Instance.playerHealthZone.transform.position);
        if(distanceFromHealth < 5f) {
            GameManager.Instance.removeOneLife();
            Destroy(gameObject);
        } 
        else if(GameManager.Instance.playerHealthZone != null) {
            agent.SetDestination(GameManager.Instance.playerHealthZone.transform.position);
        }
    }
}
