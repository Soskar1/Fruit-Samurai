using Core.Entities.AI;
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
        [SerializeField] private Flipping _flipping;
        [SerializeField] private Transform _leftWaypoint;
        [SerializeField] private Transform _rightWaypoint;
        private IMovable _movable;

        [Header("FOV")]
        [SerializeField] private FieldOfView _fov;

        public EnemyIdleState IdleState => _idleState;
        public EnemyPatrolState PatrolState => _patrolState;
        public EnemyChaseState ChaseState => _chaseState;
        public EnemyAttackState AttackState => _attackState;

        private void Awake()
        {
            _movable = GetComponent<IMovable>();
            InitializeStates();

            _currentState = _idleState;
            _currentState.EnterState();
        }

        private void OnEnable() => _fov.TargetFound += ActivateChasing;
        private void OnDisable() => _fov.TargetFound -= ActivateChasing;

        private void InitializeStates()
        {
            _idleState = new EnemyIdleState(this, _idleTime);
            _patrolState = new EnemyPatrolState(this, _flipping, _movable, _leftWaypoint.position, _rightWaypoint.position);
            _attackState = new EnemyAttackState(this);
        }

        private void Update() => _currentState.UpdateState();
        private void FixedUpdate() => _currentState.FixedUpdateState();

        public void SetState(EnemyBaseState state)
        {
            _currentState.ExitState();
            _currentState = state;
            state.EnterState();
        }

        private void ActivateChasing(Transform target)
        {
            _fov.enabled = false;
            _fov.TargetFound -= ActivateChasing;

            _chaseState = new EnemyChaseState(this, target);
            SetState(_chaseState);
        }
    }
}