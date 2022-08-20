using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Entities.AI
{
    public class PatrolPoints : MonoBehaviour
    {
        [SerializeField] private List<Transform> _patrolPoints;
        private int _currentPoint = 0;

        public Transform CurrentPoint => _patrolPoints[_currentPoint];

        public Transform GetNext()
        {
            _currentPoint = (_currentPoint + 1) % _patrolPoints.Count;
            return _patrolPoints[_currentPoint];
        }

        public bool HasReached(PatrolEnemy enemy)
        {
            if (Vector2.Distance(transform.position, CurrentPoint.position) <= enemy.StopDistance)
                return true;

            return false;
        }
    }
}