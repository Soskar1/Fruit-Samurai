using UnityEngine;
using System;

namespace Core.Entities
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 1;
        private int _currentHealth;

        private bool _invulnerable = false;
        public bool Invulnerable { get => _invulnerable; set => _invulnerable = value; }

        public Action CurrentHealthChanged;

        private void OnEnable() => _currentHealth = _maxHealth;

        public void TryTakeDamage()
        {
            if (_invulnerable)
                return;

            _currentHealth--;
            CurrentHealthChanged?.Invoke();

            if (_currentHealth <= 0)
                Die();
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}