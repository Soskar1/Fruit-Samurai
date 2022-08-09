using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Entities
{
    [RequireComponent(typeof(Jumping))]
    [RequireComponent(typeof(GroundCheck))]
    [RequireComponent(typeof(Flipping))]
    public class Player : Entity
    {
        [SerializeField] private Input _input;
        [SerializeField] private Jumping _jumping;
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private Flipping _flipping;

        private void OnEnable()
        {
            _input.Controls.Player.Jump.performed += TryJump;
        }

        private void OnDisable()
        {
            _input.Controls.Player.Jump.performed -= TryJump;
        }

        private void Update()
        {
            if (_flipping.FacingRight && _input.MovementInput < 0 ||
                !_flipping.FacingRight && _input.MovementInput > 0)
                _flipping.Flip();
        }

        private void FixedUpdate() => Move();

        public override void Move() => _movable.Move(_input.MovementInput);

        private void TryJump(InputAction.CallbackContext ctx)
        {
            if (_groundCheck.CheckForGround())
                _jumping.Jump();
        }
    }
}