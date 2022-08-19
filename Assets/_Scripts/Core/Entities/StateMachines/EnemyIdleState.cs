using UnityEngine;

namespace Core.Entities.StateMachines
{
    public class EnemyIdleState : EnemyBaseState
    {
        private float _idleTime;
        private float _timer;

        public EnemyIdleState(Enemy enemy, float idleTime) : base(enemy)
        {
            _idleTime = idleTime;
        }

        public override void EnterState()
        {
            Debug.Log("Idle");
            _timer = _idleTime;
        }

        public override void UpdateState()
        {
            if (_timer <= 0)
            {
                _enemy.SetState(_enemy.PatrolState);
                _timer = _idleTime;
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }

        public override void FixedUpdateState()
        {

        }

        public override void ExitState()
        {

        }
    }
}