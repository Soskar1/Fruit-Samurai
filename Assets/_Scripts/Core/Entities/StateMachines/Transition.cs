using UnityEngine;

namespace Core.Entities.StateMachines
{
    [CreateAssetMenu(menuName = "FSM/Transition")]
    public class Transition : ScriptableObject
    {
        public Decision Decision;
        public BaseState TrueState;
        public BaseState FalseState;

        public void Execute(BaseStateMachine stateMachine)
        {
            if (Decision.Decide(stateMachine) && !(TrueState is RemainInState))
                stateMachine.CurrentState = TrueState;
            else if (!Decision.Decide(stateMachine) && !(FalseState is RemainInState))
                stateMachine.CurrentState = FalseState;
        }
    }
}