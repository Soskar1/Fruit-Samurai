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

        [Header("Chase State")]
        [SerializeField] private float _attackDistance;

        [Header("Attack State")]
        [SerializeField] private float _attackTime;
        [SerializeField] private Transform _firstAreaPoint;
        [SerializeField] private Transform _secondAreaPoint;
        [SerializeField] private LayerMask _enemyLayer;

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
            _attackState = new EnemyAttackState(this, _attackTime, _firstAreaPoint, _secondAreaPoint, _enemyLayer);
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

            _chaseState = new EnemyChaseState(this, _flipping, _movable, target, _attackDistance);
            SetState(_chaseState);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + _attackDistance, transform.position.y));
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x - _attackDistance, transform.position.y));

            Gizmos.color = Color.cyan;

            Gizmos.DrawLine(_firstAreaPoint.position, new Vector2(_secondAreaPoint.position.x, _firstAreaPoint.position.y));
            Gizmos.DrawLine(new Vector2(_secondAreaPoint.position.x, _firstAreaPoint.position.y), _secondAreaPoint.position);
            Gizmos.DrawLine(_secondAreaPoint.position, new Vector2(_firstAreaPoint.position.x, _secondAreaPoint.position.y));
            Gizmos.DrawLine(new Vector2(_firstAreaPoint.position.x, _secondAreaPoint.position.y), _firstAreaPoint.position);
        }
    }
}