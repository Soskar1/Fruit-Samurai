using UnityEngine;

namespace Core.Entities.StateMachines
{
    public class BaseState : ScriptableObject
    {
        public virtual void Execute(BaseStateMachine machine) { }
    }
}