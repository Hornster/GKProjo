using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Services.Projectiles;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    /// <summary>
    /// Applied to ground - allows for various collision with ground effects.
    /// </summary>
    public class OnCollisionGround : MonoBehaviour
    {
        [SerializeField]
        private string objectsToDestroyTags;
        private void OnTriggerEnter2D(Collider2D otherObject)
        {
            IProjectile onCollision = otherObject.GetComponent<IProjectile>();

            if (onCollision != null)
            {
                if (!onCollision.CanPenetrate && otherObject.tag == this.objectsToDestroyTags)
                {
                    Destroy(otherObject.gameObject);
                }
            }
            else if(otherObject.tag == this.objectsToDestroyTags)
            {
                Destroy(otherObject.gameObject);
            }

        }
    }
}
