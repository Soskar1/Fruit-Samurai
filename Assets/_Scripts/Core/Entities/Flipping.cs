using UnityEngine;

namespace Core.Entities
{
    public class Flipping : MonoBehaviour
    {
        [SerializeField] private bool _facingRight = true;
        public bool FacingRight => _facingRight;

        private Transform _transform;
        private Vector3 _visualRotation = new Vector3(0,180,0);

        private void Awake() => _transform = transform;

        public void Flip()
        {
            _facingRight = !_facingRight;

            transform.Rotate(_visualRotation);
        }

        public void FaceTheTarget(Vector2 target)
        {
            if (FacingRight && target.x < _transform.position.x ||
                !FacingRight && target.x > _transform.position.x)
                Flip();
        }

        public void FaceTheTarget(Transform target) => FaceTheTarget(target.position);
    }
}