using UnityEngine;
using System;

namespace Core.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Rolling : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb2d;
        [SerializeField] private float _force;

        [SerializeField] private float _delay;
        private float _timer;

        private bool _cooldown = false;
        public bool Cooldown => _cooldown;

        private bool _isRolling = false;
        public bool IsRolling => _isRolling;

        public Action OnRollEnded;

        private void Awake() => _timer = _delay;

        private void Update()
        {
            if (!_cooldown)
                return;

            if (_timer <= 0)
            {
                _cooldown = false;

                _timer = _delay;
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }

        public void Roll(int direction)
        {
            _isRolling = true;

            _rb2d.velocity = Vector2.zero;
            _rb2d.AddForce(Vector2.right * direction * _force, ForceMode2D.Impulse);
        }

        public void EndRoll()
        {
            _isRolling = false;
            _cooldown = true;
            OnRollEnded?.Invoke();
        }
    }
}