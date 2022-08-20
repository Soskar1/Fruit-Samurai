using UnityEngine;

namespace Core.Entities.StateMachines
{
    public abstract class FSMAction : ScriptableObject
    {
        public abstract void Execute(BaseStateMachine stateMachine);
    }
}