using UnityEngine;

namespace Core.Entities.StateMachines
{
    [CreateAssetMenu(menuName = "FSM/Decision/In Sight")]
    public class InSightDecision : Decision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            var fov = stateMachine.GetComponent<FieldOfView>();
            return fov.TryFindTarget();
        }
    }
}