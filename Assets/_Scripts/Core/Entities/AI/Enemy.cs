using Core.Entities.Movement;
using UnityEngine;

namespace Core.Entities.AI
{
    public abstract class Enemy : Entity
    {
        protected Transform _target;

        private void Update()
        {
            if (_target == null) return;

            _flipping.FaceTheTarget(_target);
        }

        public void SetDestination(Transform point) => _target = point;
    }
}