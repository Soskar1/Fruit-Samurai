using UnityEngine;

namespace Core.Entities.StateMachines
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(BaseStateMachine stateMachine);
    }
}