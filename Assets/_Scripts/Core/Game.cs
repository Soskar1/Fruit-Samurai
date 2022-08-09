using UnityEngine;

namespace Core
{
    public class Game : MonoBehaviour
    {
        private void Awake()
        {
            //Player & Enemy
            Physics2D.IgnoreLayerCollision(7, 8);
        }
    }
}