using UnityEngine;
using System;

namespace Core.Entities.AI
{
    public class FieldOfView : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private LayerMask _obstacleLayer;
        private Vector2 _center;
        private Transform _target;

        public Action<Transform> TargetFound;

        private void Awake() => _center = transform.position;

        private void LateUpdate()
        {
            if (_target != null)
                return;

            Collider2D overlapInfo = Physics2D.OverlapCircle(_center, _radius, _targetLayer);
            if (overlapInfo == null)
                return;

            Vector2 direction = (Vector2)overlapInfo.transform.position - _center;
            RaycastHit2D hitInfo = Physics2D.Raycast(_center, direction, _radius, _obstacleLayer);

            if (hitInfo.collider != null)
                return;

            _target = overlapInfo.transform;
            TargetFound?.Invoke(_target);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            const float TWO_PI = Mathf.PI * 2;
            float step = TWO_PI / 36;
            float theta = 0;
            float x = _radius * Mathf.Cos(theta);
            float y = _radius * Mathf.Sin(theta);
            Vector3 pos = transform.position + new Vector3(x, y, 0);
            Vector3 newPos;
            Vector3 lastPos = pos;
            for (theta = step; theta < TWO_PI; theta += step)
            {
                x = _radius * Mathf.Cos(theta);
                y = _radius * Mathf.Sin(theta);
                newPos = transform.position + new Vector3(x, y, 0);
                Gizmos.DrawLine(pos, newPos);
                pos = newPos;
            }

            Gizmos.DrawLine(pos, lastPos);
        }
    }
}