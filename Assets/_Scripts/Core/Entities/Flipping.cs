using UnityEngine;

namespace Core.Entities
{
    public class Flipping : MonoBehaviour
    {
        [SerializeField] private bool _facingRight = true;
        public bool FacingRight => _facingRight;


        private Vector3 _visualRotation = new Vector3(0,180,0);

        public void Flip()
        {
            _facingRight = !_facingRight;

            transform.Rotate(_visualRotation);
        }
    }
}