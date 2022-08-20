using Core.Entities.AI;
using UnityEngine;

namespace Core.Entities.StateMachines
{
    [CreateAssetMenu(menuName = "FSM/Actions/Patrol")]
    public class PatrolAction : FSMAction
    {
        public override void Execute(BaseStateMachine stateMachine)
        {
            var enemy = stateMachine.GetComponent<PatrolEnemy>();
            var patrolPoints = stateMachine.GetComponent<PatrolPoints>();

            if (patrolPoints.HasReached(enemy))
                enemy.SetDestination(patrolPoints.GetNext());
        }
    }
}