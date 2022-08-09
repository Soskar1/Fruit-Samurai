using UnityEngine;
using System;

namespace Core.Entities
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 1;
        private int _currentHealth;

        public Action CurrentHealthChanged;

        private void OnEnable() => _currentHealth = _maxHealth;

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
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