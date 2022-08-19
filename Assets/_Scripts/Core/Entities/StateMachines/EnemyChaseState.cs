using UnityEngine;

namespace Core.Entities.StateMachines
{
    public class EnemyChaseState : EnemyBaseState
    {
        private Transform _target;

        public EnemyChaseState(Enemy enemy, Transform target) : base(enemy)
        {
            _target = target;
        }

        public override void EnterState()
        {
            Debug.Log("Chasing");
        }

        public override void UpdateState()
        {
            
        }

        public override void FixedUpdateState()
        {
            
        }

        public override void ExitState()
        {
            
        }
    }
}