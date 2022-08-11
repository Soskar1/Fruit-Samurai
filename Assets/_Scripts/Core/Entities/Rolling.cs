using UnityEngine;

namespace Core.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Rolling : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb2d;
        [SerializeField] private float _force;

        private bool _isRolling = false;
        public bool IsRolling => _isRolling;

        public void Roll(int direction)
        {
            _isRolling = true;

            _rb2d.velocity = Vector2.zero;
            _rb2d.AddForce(Vector2.right * direction * _force, ForceMode2D.Impulse);
        }

        public void EndRoll() => _isRolling = false;
    }
}