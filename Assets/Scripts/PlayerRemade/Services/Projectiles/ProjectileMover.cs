using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    public class ProjectileMover : MonoBehaviour, IProjectileMover
    {
        private bool _isMoving = false;
        Rigidbody2D rb;
        public float velocity = 30;
        private Vector3 moveDirection;
        Vector3 playerVelocity;
        
        // Use this for initialization
   
        public void Initialize ()
        {
            rb = GetComponent<Rigidbody2D>();
        }
	
        public void SetMoveDirection(Vector2 direction, Vector2 playerVelocity)
        {
            moveDirection = new Vector3(direction.x, direction.y, 0);
            this.playerVelocity = new Vector3(playerVelocity.x, playerVelocity.y, 0);
            Vector3 plVelocity = playerVelocity;
            plVelocity += moveDirection * velocity;
            rb.velocity = plVelocity;
        }
    }
}

/*TO DO
 -dodac auto niszczenie obiektu po uplywie pewnego czasu, jezeli obiekt nie zostal jeszcze zniszczony*/
