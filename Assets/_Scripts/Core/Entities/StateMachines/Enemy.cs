using Core.Entities.Movement;
using UnityEngine;

namespace Core.Entities.StateMachines
{
    public class Enemy : MonoBehaviour
    {
        private EnemyBaseState _currentState;
        private EnemyIdleState _idleState;
        private EnemyPatrolState _patrolState;
        private EnemyChaseState _chaseState;
        private EnemyAttackState _attackState;

        [Header("Idle State")]
        [SerializeField] private float _idleTime;

        [Header("Patrol State")]
        [SerializeField] private Patrol _patrol;
        [SerializeField] private float _patrolTime;
        private IMovable _movable;

        public EnemyIdleState IdleState => _idleState;
        public EnemyPatrolState PatrolState => _patrolState;
        public EnemyChaseState ChaseState => _chaseState;
        public EnemyAttackState AttackState => _attackState;

        private void Awake()
        {
            _movable = GetComponent<IMovable>();
            InitializeStates();
        }

        private void Start() => SetState(_idleState);

        private void InitializeStates()
        {
            _idleState = new EnemyIdleState(this, _idleTime);
            _patrolState = new EnemyPatrolState(this, _patrolTime, _movable, _patrol);
            _chaseState = new EnemyChaseState(this);
            _attackState = new EnemyAttackState(this);
        }

        private void Update() => _currentState.UpdateState();

        public void SetState(EnemyBaseState state)
        {
            _currentState = state;
            state.EnterState();
        }
    }
}