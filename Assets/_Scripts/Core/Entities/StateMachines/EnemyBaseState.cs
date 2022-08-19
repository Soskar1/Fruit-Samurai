using UnityEngine;

namespace Core.Entities.StateMachines
{
    public abstract class EnemyBaseState
    {
        protected Enemy _enemy;

        public EnemyBaseState(Enemy enemy) => _enemy = enemy;

        public abstract void EnterState();
        public abstract void UpdateState();
    }
}