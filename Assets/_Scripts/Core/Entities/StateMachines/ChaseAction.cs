using Core.Entities.AI;
using UnityEngine;

namespace Core.Entities.StateMachines
{
    [CreateAssetMenu(menuName = "FSM/Actions/Chase")]
    public class ChaseAction : FSMAction
    {
        public override void Execute(BaseStateMachine stateMachine)
        {
            var enemy = stateMachine.GetComponent<Enemy>();
            var fov = stateMachine.GetComponent<FieldOfView>();

            enemy.SetDestination(fov.Target);
        }
    }
}