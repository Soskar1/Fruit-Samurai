using UnityEngine;

namespace Core
{
    public class Input : MonoBehaviour
    {
        private Controls _controls;
        private float _movementInput;
        private Vector2 _mousePosition;

        public Controls Controls => _controls;
        public float MovementInput => _movementInput;
        public Vector2 MousePosition => _mousePosition;

        private void Awake() => _controls = new Controls();
        private void OnEnable() => _controls.Enable();
        private void OnDisable() => _controls.Disable();

        private void Update()
        {
            _movementInput = _controls.Player.Movement.ReadValue<float>();
            _mousePosition = _controls.Player.MousePosition.ReadValue<Vector2>();
        }
    }
}
