using UnityEngine;

namespace Core.Entities
{
    public class Patrol : MonoBehaviour
    {
        [SerializeField] private Transform _waypoint;
        private Transform _transform;

        private void Awake() => _transform = transform;

        public float GetMovementDirection()
        {
            return -1;
        }
    }
}