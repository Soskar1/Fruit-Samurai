using UnityEngine;

namespace Core.Entities.AI
{
    public class Chasing : MonoBehaviour
    {
        [SerializeField] private FieldOfView _fov;
        [SerializeField] private Flipping _flipping;
        private Transform _target;
        private Transform _transform;

        private bool _canChase = false;
        public bool CanChase => _canChase;

        private const int _RIGHT_MOVEMENT_DIRECTION = 1;
        private const int _LEFT_MOVEMENT_DIRECTION = -1;

        private void Awake() => _transform = transform;

        private void OnEnable() => _fov.TargetFound += SetTarget;
        private void OnDisable() => _fov.TargetFound -= SetTarget;

        private void Update()
        {
            if (_target == null)
                return;

            if (_target.position.x > _transform.position.x && !_flipping.FacingRight ||
                _target.position.x < _transform.position.x && _flipping.FacingRight)
                _flipping.Flip();
        }

        private void SetTarget(Transform newTarget)
        {
            _target = newTarget;
            _canChase = true;
        }

        public void StartChasing() => _canChase = true;
        public void StopChasing() => _canChase = false;

        public float GetDistanceToTarget() => Vector2.Distance(_target.position, _transform.position);

        public float GetMovementDirection()
        {
            if (_target.position.x > _transform.position.x)
                return _RIGHT_MOVEMENT_DIRECTION;
            else
                return _LEFT_MOVEMENT_DIRECTION;
        }
    }
}